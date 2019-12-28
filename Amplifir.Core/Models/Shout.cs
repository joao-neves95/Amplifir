using System;
using System.Collections.Generic;
using System.Text;

namespace Amplifir.Core.Models
{
    public class Shout
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public string Content { get; set; }

        public DateTime CreateDate { get; set; }

        public int LikesNum { get; set; }

        public int DislikesNum { get; set; }

        public List<Hashtag> Hashtags { get; set; }

        public List<ShoutAsset> Assets { get; set; }
    }
}
