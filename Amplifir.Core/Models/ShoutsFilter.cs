using Amplifir.Core.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Amplifir.Core.Models
{
    public class ShoutsFilter
    {
        public ShoutsFilter(FilterType filterBy)
        {
            this.FilteredBy = filterBy;
            this.Hashtags = Array.Empty<string>();
        }

        public FilterType FilteredBy { get; set; }

        public string[] Hashtags { get; set; }
    }
}
