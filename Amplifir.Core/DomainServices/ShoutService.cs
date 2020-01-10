using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Amplifir.Core.Enums;
using Amplifir.Core.Interfaces;
using Amplifir.Core.Entities;
using Amplifir.Core.Models;

namespace Amplifir.Core.DomainServices
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage( "Reliability", "CA2007:Consider calling ConfigureAwait on the awaited task", Justification = "<Pending>" )]
    public class ShoutService : IShoutService
    {
        public ShoutService(IShoutStore shoutStore, IAppSettings appSettings, IBadWordsService badWordsService)
        {
            this._shoutStore = shoutStore;
            this._appSettings = appSettings;
            this._badWordsService = badWordsService;
        }

        private const char HASHTAG_CHAR = '#';
        private const string ALLOWED_HASHTAG_NONAPHANUM_CHARS = "_";

        private readonly IShoutStore _shoutStore;
        private readonly IBadWordsService _badWordsService;
        private readonly IAppSettings _appSettings;

        #region PUBLIC FAÇADE METHODS

        public async Task<CreateShoutResult> CreateAsync(Shout newShout)
        {
            if (newShout.Content.Length > _appSettings.Shout_MaxLength)
            {
                return new CreateShoutResult() { State = CreateShoutState.ContentTooLong, NewShout = null };
            }

            newShout.Hashtags = this.GetHashtagsFromShoutContent( newShout.Content ).ToList();
            List<string> hashtagsThatExist = await _shoutStore.GetHashtagsAsync( newShout.Hashtags );
            newShout.Hashtags.RemoveAll( hashtag => hashtagsThatExist.Contains( hashtag ) );

            if (newShout.Hashtags.Count > 0)
            {
                await _shoutStore.CreateHashtagAsync( newShout.Hashtags );
            }

            newShout.Content = await _badWordsService.CleanAsync( newShout.Content );
            newShout.Id = await _shoutStore.CreateAsync( newShout );
            await _shoutStore.AddShoutToExistingHashtag( newShout.Id, hashtagsThatExist );

            return new CreateShoutResult() { State = CreateShoutState.Success, NewShout = newShout };
        }

        public async Task<CreateReactionResult> CreateReactionAsync(EntityType entityType, int entityId, int userId, short reactionTypeId)
        {
            CreateReactionResult createReactionResult = new CreateReactionResult()
            {
                EntityType = entityType,
                ReactionId = -1
            };

            if (entityId < 0 || userId < 0 || reactionTypeId < 0)
            {
                createReactionResult.State = CreateReactionState.BadRequest;
                return createReactionResult;
            }

            if (await _shoutStore.UserReactionExistsAsync( entityId, userId ))
            {
                createReactionResult.State = CreateReactionState.ReactionExists;
                return createReactionResult;
            }

            createReactionResult.ReactionId = await _shoutStore.CreateReactionAsync( entityType, entityId, userId, reactionTypeId );
            createReactionResult.State = CreateReactionState.Success;
            return createReactionResult;
        }

        public async Task DeleteAsync( int shoutId, int userId )
        {
            if (shoutId < 0 || userId < 0)
            {
                throw new ArgumentOutOfRangeException();
            }

            await _shoutStore.DeleteAsync( shoutId, userId );
        }

        public async Task DeleteReactionAsync(EntityType entityType, int entityId, int userId)
        {
            if (entityId < 0 || userId < 0)
            {
                throw new ArgumentOutOfRangeException();
            }

            await _shoutStore.DeleteReactionAsync( entityType, entityId, userId );
        }

        #endregion PUBLIC FAÇADE METHODS

        #region PRIVATE METHODS

        private string[] GetHashtagsFromShoutContent(string content)
        {
            if (String.IsNullOrEmpty( content ))
            {
                return Array.Empty<string>();
            }

            // In case the string starts with an hashtag (I.e: "#"),
            // the array will start with an empty string before the
            // first hashtag.
            string[] hashtags = content.Split( ShoutService.HASHTAG_CHAR );

            // There are no hashtags.
            if (hashtags.Length <= 1)
            {
                return Array.Empty<string>();
            }

            // This is for trimming everything that's not part of the hashtag.
            int i;
            int j;
            string thisHashtag;
            char currentChar;
            for (i = 0; i < hashtags.Length - 1; ++i)
            {
                thisHashtag = "";

                for (j = 0; j < hashtags[i + 1].Length; ++j)
                {
                    currentChar = hashtags[i + 1][j];

                    if ( Char.IsLetterOrDigit( currentChar ) || ShoutService.ALLOWED_HASHTAG_NONAPHANUM_CHARS.Contains( currentChar ) )
                    {
                        thisHashtag += currentChar;
                    }
                    else
                    {
                        // Jump to the next hashtag.
                        break;
                    }
                }

                hashtags[i] = thisHashtag;
            }

            // To account for the first string.
            Array.Resize( ref hashtags, hashtags.Length - 1 );
            return hashtags;
        }

        #endregion PRIVATE METHODS
    }
}
