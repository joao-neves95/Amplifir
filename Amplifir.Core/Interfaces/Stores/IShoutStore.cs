/*
 * Copyright (c) 2019 - 2020 João Pedro Martins Neves (SHIVAYL) - All Rights Reserved.
 *
 * Amplifir and all its content is licensed under the GNU Lesser General Public License (LGPL),
 * version 3, located in the root of this project, under the name "LICENSE.md".
 *
 */

using System.Threading.Tasks;
using Amplifir.Core.Entities;
using Amplifir.Core.Enums;

namespace Amplifir.Core.Interfaces
{
    /// <summary>
    ///
    /// This must not be used directly from the API, only to be used internally.
    /// Must be encapsulated within a service (façade) with the business logic
    /// and consumed from there.
    /// 
    /// </summary>
    public interface IShoutStore : IHashtagStore, ICommentStore
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
        /// Creates a Shout reaction, returning its ID.
        /// 
        /// </summary>
        /// <param name="entityType"></param>
        /// <param name="entityId"> The ID of the Shout or Comment. </param>
        /// <param name="userId"></param>
        /// <param name="reactionTypeId"> Use the ReactionTypeId class in Core/Entities/ </param>
        /// <returns></returns>
        Task<int> CreateReactionAsync( EntityType entityType, int entityId, int userId, short reactionTypeId );

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

        Task<ShoutReaction> GetShoutReactionAsync( int shoutId, int userId );

        /// <summary>
        /// 
        /// Deletes everything related to a Shout.
        /// 
        /// </summary>
        /// <param name="shoutId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<int> DeleteAsync( int shoutId, int userId );

        /// <summary>
        ///
        /// Deletes the reation, returning the number of affected rows.
        /// Should return 1, if successful.
        /// 
        /// </summary>
        /// <param name="entityType"></param>
        /// <param name="id"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<int> DeleteReactionByIdAsync( EntityType entityType, int id, int userId );

        Task<bool> UserReactionExistsAsync( int shoutId, int userId );
    }
}
