/*
 * Copyright (c) 2019 - 2020 João Pedro Martins Neves (SHIVAYL) - All Rights Reserved.
 *
 * Amplifir and all its content is licensed under the GNU Lesser General Public License (LGPL),
 * version 3, located in the root of this project, under the name "LICENSE.md".
 *
 */

using System.Collections.Generic;
using System.Threading.Tasks;
using Amplifir.Core.Entities;
using Amplifir.Core.Enums;

namespace Amplifir.Core.Interfaces
{
    public interface ICommentStore
    {
        /// <summary>
        ///
        /// Creates a new comment, returning its ID.
        ///
        /// </summary>
        /// <param name="newComment"></param>
        /// <returns></returns>
        Task<int> CreateCommentAsync( Comment newComment );

        Task<List<Comment>> GetCommentsByShoutIdAsync( int shoutId, int lastId = 0, int limit = 10 );

        Task<CommentReaction> GetCommentReactionAsync( int commentId, int userId );

        Task<int> DeleteCommentByIdAsync( int id, int userId );
    }
}
