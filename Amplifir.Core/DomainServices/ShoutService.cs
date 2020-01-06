using System;
using System.Linq;
using System.Threading.Tasks;
using Amplifir.Core.Entities;
using Amplifir.Core.Interfaces;

namespace Amplifir.Core.DomainServices
{
    public class ShoutService : IShoutService
    {
        private const string ALLOWED_HASHTAG_NONAPHANUM_CHARS = "_";

        public Task<bool> Create(Shout newShout)
        {
            newShout.Hashtags = this.GetHashtags( newShout.Content ).ToList();

            throw new NotImplementedException();
        }

        #region PRIVATE METHODS

        private string[] GetHashtags(string content)
        {
            if (String.IsNullOrEmpty( content ))
            {
                return Array.Empty<string>();
            }

            // In case the string starts with an hashtag (I.e: "#"),
            // the array will start with an empty string before the
            // first hashtag.
            string[] hashtags = content.Split( '#' );

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

            // To account for the first char.
            Array.Resize( ref hashtags, hashtags.Length - 1 );
            return hashtags;
        }

        #endregion PRIVATE METHODS
    }
}
