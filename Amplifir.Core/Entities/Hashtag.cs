using System;
using System.Collections.Generic;
using System.Text;

namespace Amplifir.Core.Entities
{
    public class Hashtag
    {
        public long Id { get; set; }

        public string Content { get; set; }

        public int ShoutCount { get; set; }
    }
}
