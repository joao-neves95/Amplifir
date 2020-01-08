using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Amplifir.Core.Entities;

namespace Amplifir.Core.Interfaces
{
    public interface IShoutStore : IHashtagStore
    {
        /// <summary>
        /// 
        /// Inserts a new Shout and returns its ID.
        /// 
        /// </summary>
        /// <param name="newShout"></param>
        /// <returns></returns>
        Task<int> CreateAsync( Shout newShout );

        /// <summary>
        /// 
        /// Get Shout by ID.
        /// 
        /// </summary>
        /// <param name="shoutId"></param>
        /// <returns></returns>
        Task<Shout> GetByIdAsync( int shoutId );

        /// <summary>
        /// 
        /// Get the last paginated Shouts of a user by ID.
        /// 
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="lastId"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        Task<Shout> GetByUserIdAsync( int userId, int lastId = 0, short limit = 10 );

        /// <summary>
        /// 
        /// Get the last paginated Shouts from all the users a user follows.
        /// 
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="lastId"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        Task<Shout> GetFollowingShoutsByUserIdAsync( int userId, int lastId = 0, short limit = 10 );
    }
}
