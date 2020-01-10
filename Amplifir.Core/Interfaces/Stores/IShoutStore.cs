using System.Threading.Tasks;
using Amplifir.Core.Enums;
using Amplifir.Core.Entities;

namespace Amplifir.Core.Interfaces
{
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
        /// <param name="shoutId"></param>
        /// <param name="userId"></param>
        /// <param name="reactionTypeId"> Use the ReactionTypeId class in Core/Entities/ </param>
        /// <returns></returns>
        Task<int> CreateReactionAsync( EntityType entityType, int shoutId, int userId, short reactionTypeId );

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


        /// <summary>
        /// 
        /// Deletes everything related to the Shout.
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
        /// <param name="entityId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<int> DeleteReactionAsync( EntityType entityType, int entityId, int userId );

        Task<bool> UserReactionExistsAsync( int shoutId, int userId );
    }
}
