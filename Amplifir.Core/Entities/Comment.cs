using System;
using Amplifir.Core.DTOs;

namespace Amplifir.Core.Entities
{
    public class Comment : NewCommentDTO
    {
        public int Id { get; set; }

        public DateTime CreateDate { get; set; }

        public int LikesCount { get; set; }

        public int DislikesCount { get; set; }
    }
}
