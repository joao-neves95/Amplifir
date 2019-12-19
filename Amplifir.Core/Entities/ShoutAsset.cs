using System;
using System.Collections.Generic;
using System.Text;

namespace Amplifir.Core.Entities
{
    public class ShoutAsset
    {
        public int Id { get; set; }

        public int ShoutId { get; set; }

        public short AssetTypeId { get; set; }

        public string URL { get; set; }
    }
}
