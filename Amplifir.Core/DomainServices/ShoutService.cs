﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Amplifir.Core.Entities;
using Amplifir.Core.Interfaces;

namespace Amplifir.Core.DomainServices
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage( "Reliability", "CA2007:Consider calling ConfigureAwait on the awaited task", Justification = "<Pending>" )]
    public class ShoutService : IShoutService
    {
        public ShoutService(IShoutStore shoutStore)
        {
            this._shoutStore = shoutStore;
        }

        private const char HASHTAG_CHAR = '#';
        private const string ALLOWED_HASHTAG_NONAPHANUM_CHARS = "_";

        private readonly IShoutStore _shoutStore;

        public async Task CreateAsync(Shout newShout)
        {
            newShout.Hashtags = this.GetHashtagsFromShoutContent( newShout.Content ).ToList();
            List<string> hashtagsThatExist = await _shoutStore.GetHashtagsAsync( newShout.Hashtags );
            newShout.Hashtags.RemoveAll( hashtag => hashtagsThatExist.Contains( hashtag ) );

            if (newShout.Hashtags.Count > 0)
            {
                // Normalize.
                newShout.Hashtags = (List<string>)newShout.Hashtags.Select( hashtag => hashtag.ToLower().Trim() );
                await _shoutStore.CreateHashtagAsync( newShout.Hashtags );
            }

            int insertedShoutId = await _shoutStore.CreateAsync( newShout );
            await _shoutStore.AddShoutToExistingHashtag( insertedShoutId, hashtagsThatExist );
        }

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

                    if (Char.IsLetterOrDigit( currentChar ) || ShoutService.ALLOWED_HASHTAG_NONAPHANUM_CHARS.Contains( currentChar ))
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
