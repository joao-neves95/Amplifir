/*
 * Copyright (c) 2019 - 2020 João Pedro Martins Neves (SHIVAYL) - All Rights Reserved.
 *
 * Amplifir and all its content is licensed under the GNU Lesser General Public License (LGPL),
 * version 3, located in the root of this project, under the name "LICENSE.md".
 *
 */

using System.Threading.Tasks;
using Amplifir.Core.Enums;
using Amplifir.Core.Entities;
using Amplifir.Core.Models;

namespace Amplifir.Core.Interfaces
{
    public interface IShoutService
    {
        Task<CreateShoutResult> CreateAsync( Shout newShout );

        Task<CreateCommentResult> CreateCommentAsync( Comment newComment );

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

        Task DeleteCommentAsync( int commentId, int userId );

        Task DeleteReactionAsync( EntityType entityType, int entityId, int userId );
    }
}
