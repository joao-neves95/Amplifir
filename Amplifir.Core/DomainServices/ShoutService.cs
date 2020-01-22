/*
 * Copyright (c) 2019 - 2020 João Pedro Martins Neves (SHIVAYL) - All Rights Reserved.
 *
 * Amplifir and all its content is licensed under the GNU Lesser General Public License (LGPL),
 * version 3, located in the root of this project, under the name "LICENSE.md".
 *
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Amplifir.Core.Enums;
using Amplifir.Core.Interfaces;
using Amplifir.Core.Entities;
using Amplifir.Core.Models;
using Amplifir.Core.DTOs;

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

        #region CREATE

        public async Task<CreateShoutResult> CreateAsync(Shout newShout)
        {
            CreateShoutResult createShoutResult = new CreateShoutResult()
            {
                NewShout = null
            };

            if (newShout.Content.Length < _appSettings.Shout_MinLength)
            {
                createShoutResult.State = CreateShoutState.ContentTooSmall;
                return createShoutResult;
            }

            if (newShout.Content.Length > _appSettings.Shout_MaxLength)
            {
                createShoutResult.State = CreateShoutState.ContentTooLong;
                return createShoutResult;
            }

            newShout.Content = await _badWordsService.CleanAsync( newShout.Content );
            List<string> newShoutHashtags = this.GetHashtagsFromShoutContent( newShout.Content ).ToList();
            List<string> hashtagsThatExist = await _shoutStore.GetHashtagsAsync( newShoutHashtags );
            newShoutHashtags.RemoveAll( hashtag => hashtagsThatExist.Contains( hashtag ) );

            if (newShoutHashtags.Count > 0)
            {
                await _shoutStore.CreateHashtagAsync( newShoutHashtags );
            }

            newShout.Id = await _shoutStore.CreateAsync( newShout );
            newShout.CreateDate = DateTime.UtcNow;

            if (hashtagsThatExist.Count > 0)
            {
                await _shoutStore.AddShoutToExistingHashtag( newShout.Id, hashtagsThatExist );
            }

            createShoutResult.State = CreateShoutState.Success;
            createShoutResult.NewShout = newShout;
            return createShoutResult;
        }

        public async Task<CreateCommentResult> CreateCommentAsync(Comment newComment)
        {
            CreateCommentResult createCommentResult = new CreateCommentResult()
            {
                NewComment = null
            };

            if (newComment.Content.Length < _appSettings.Shout_MinLength)
            {
                createCommentResult.State = CreateShoutState.ContentTooSmall;
                return createCommentResult;
            }

            if (newComment.Content.Length > _appSettings.Shout_MaxLength)
            {
                createCommentResult.State = CreateShoutState.ContentTooLong;
                return createCommentResult;
            }

            newComment.Content = await _badWordsService.CleanAsync( newComment.Content );
            newComment.Id = await _shoutStore.CreateCommentAsync( newComment );
            newComment.ShoutId = newComment.ShoutId;
            newComment.UserId = newComment.UserId;
            newComment.CreateDate = DateTime.UtcNow;

            createCommentResult.State = CreateShoutState.Success;
            createCommentResult.NewComment = newComment;
            return createCommentResult;
        }

        public async Task<CreateReactionResult> CreateReactionAsync(EntityType entityType, int entityId, int userId, short reactionTypeId)
        {
            CreateReactionResult createReactionResult = new CreateReactionResult()
            {
                EntityType = entityType,
                EntityId = entityId
            };

            if (entityId < 0 || userId < 0 || reactionTypeId < 0)
            {
                createReactionResult.State = CreateReactionState.BadRequest;
                return createReactionResult;
            }

            createReactionResult.Reaction = null;

            if (entityType == EntityType.Shout)
            {
                createReactionResult.Reaction = await _shoutStore.GetShoutReactionAsync( entityId, userId );
            }
            else
            {
                createReactionResult.Reaction = await _shoutStore.GetCommentReactionAsync( entityId, userId );
            }

            if (createReactionResult.Reaction != null)
            {
                if (createReactionResult.Reaction.ReactionTypeId == reactionTypeId)
                {
                    createReactionResult.State = CreateReactionState.ReactionExists;
                    return createReactionResult;
                }
                else
                {
                    // (Toggle Like/Dislike).
                    await _shoutStore.DeleteReactionByIdAsync( entityType, createReactionResult.Reaction as ReactionBase, entityId );
                }
            }
            else
            {
                createReactionResult.Reaction = entityType == EntityType.Shout ? new ShoutReaction() as IReaction : new CommentReaction() as IReaction;
                createReactionResult.Reaction.ReactionTypeId = reactionTypeId;
                createReactionResult.Reaction.UserId = userId;
            }

            createReactionResult.Reaction.Id = await _shoutStore.CreateReactionAsync( entityType, entityId, userId, reactionTypeId );
            createReactionResult.State = CreateReactionState.Success;
            return createReactionResult;
        }

        #endregion CREATE

        #region GET

        public async Task<List<Shout>> GetByUserIdAsync(int userId, int lastId = 0, short limit = 10)
        {
            if (userId < 0 || limit < 1)
            {
                throw new ArgumentOutOfRangeException();
            }

            return await this._shoutStore.GetByUserIdAsync( userId, lastId, limit );
        }

        public async Task<List<Shout>> GetAsync(ShoutsFilterDTO shoutsFilterDTO, int lastId = 0, short limit = 10)
        {
            if (lastId < 0 || limit < 1)
            {
                throw new ArgumentOutOfRangeException();
            }

            return await this._shoutStore.GetAsync( shoutsFilterDTO, lastId, limit );
        }

        public async Task<List<Comment>> GetCommentsByShoutIdAsync(int shoutId, int lastId = 0, short limit = 10)
        {
            if (shoutId < 0 || limit < 1)
            {
                throw new ArgumentOutOfRangeException();
            }

            return await this._shoutStore.GetCommentsByShoutIdAsync( shoutId, lastId, limit );
        }

        #endregion GET

        #region DELETE

        public async Task DeleteAsync( int shoutId, int userId )
        {
            if (shoutId < 0 || userId < 0)
            {
                throw new ArgumentOutOfRangeException();
            }

            await _shoutStore.DeleteAsync( shoutId, userId );
        }

        public async Task DeleteCommentAsync(int commentId, int userId)
        {
            if (commentId < 0 || userId < 0)
            {
                throw new ArgumentOutOfRangeException();
            }

            await _shoutStore.DeleteCommentByIdAsync( commentId, userId );
        }

        public async Task DeleteReactionAsync(EntityType entityType, ReactionBase reaction, int entityId)
        {
            if (entityId < 0 || entityId < 0)
            {
                throw new ArgumentOutOfRangeException();
            }

            await _shoutStore.DeleteReactionByIdAsync( entityType, reaction, entityId );
        }

        #endregion DELETE

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
