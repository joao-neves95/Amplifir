using System;
using System.Collections.Generic;
using Amplifir.Core.DTOs;

namespace Amplifir.Core.Entities
{
    public class Shout : NewShoutDTO
    {
        public int Id { get; set; }

        public DateTime CreateDate { get; set; }

        public int LikesCount { get; set; }

        public int DislikesCount { get; set; }

        public List<string> Hashtags { get; set; }

        public List<ShoutAsset> Assets { get; set; }
    }
}
