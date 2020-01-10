using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Amplifir.Core.Entities;

namespace Amplifir.Core.Interfaces
{
    public interface IHashtagStore
    {
        Task<bool> HashtagExists( string hashtag );

        /// <summary>
        /// 
        /// Selects multiple hashtags by content, returning the ones that do exist.
        /// 
        /// </summary>
        /// <param name="hashtag"></param>
        /// <returns></returns>
        Task<List<string>> GetHashtagsAsync( List<string> hashtag );

        Task<int> CreateHashtagAsync( string hashtag );

        Task<int> CreateHashtagAsync( List<string> hashtags );

        Task<int> AddShoutToExistingHashtag( int shoutId, string hashtag );

        Task<int> AddShoutToExistingHashtag( int shoutId, List<string> hashtags );
    }
}
