﻿using System;
using System.Collections.Generic;

namespace Amplifir.Core.Entities
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