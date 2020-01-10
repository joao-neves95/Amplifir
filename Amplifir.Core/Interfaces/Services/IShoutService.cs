using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Amplifir.Core.Enums;
using Amplifir.Core.Entities;
using Amplifir.Core.Models;

namespace Amplifir.Core.Interfaces
{
    public interface IShoutService
    {
        Task<CreateShoutResult> CreateAsync( Shout newShout );

        /// <summary>
        ///
        /// Creates a Shout or Comment reaction.
        /// 
        /// </summary>
        /// <param name="entityType"></param>
        /// <param name="entityId"></param>
        /// <param name="userId"></param>
        /// <param name="reactionTypeId"></param>
        /// <returns></returns>
        Task<CreateReactionResult> CreateReactionAsync( EntityType entityType, int entityId, int userId, short reactionTypeId );

        Task DeleteAsync( int shoutId, int userId );

        Task DeleteReactionAsync( EntityType entityType, int entityId, int userId );
    }
}
