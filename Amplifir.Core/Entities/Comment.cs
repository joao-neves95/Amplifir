/*
 * Copyright (c) 2019 - 2020 João Pedro Martins Neves (SHIVAYL) - All Rights Reserved.
 *
 * Amplifir and all its content is licensed under the GNU Lesser General Public License (LGPL),
 * version 3, located in the root of this project, under the name "LICENSE.md".
 *
 */

using System;
using Amplifir.Core.DTOs;

namespace Amplifir.Core.Entities
{
    public class Comment : NewCommentDTO
    {
        public int Id { get; set; }

        public int ShoutId { get; set; }

        public int UserId { get; set; }

        public DateTime CreateDate { get; set; }

        public int LikesCount { get; set; }

        public int DislikesCount { get; set; }
    }

    public static class CommentExtensions
    {
        public static Comment AddIds(this Comment comment, int shoutId, int userId)
        {
            comment.ShoutId = shoutId;
            comment.UserId = userId;
            return comment;
        }
    }
}
