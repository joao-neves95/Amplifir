using Amplifir.Core.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Amplifir.Core.DTOs
{
    public class ShoutsFilterDTO
    {
        public FilterType FilteredBy { get; set; }

        public string[] Hashtags { get; set; }
    }
}
