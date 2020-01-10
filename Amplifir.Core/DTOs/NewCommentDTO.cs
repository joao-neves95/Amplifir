using System;
using System.Collections.Generic;
using System.Text;

namespace Amplifir.Core.DTOs
{
    public class NewCommentDTO
    {
        public int ShoutId { get; set; }

        public int UserId { get; set; }

        public string Content { get; set; }
    }
}
