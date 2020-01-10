using System;
using System.Collections.Generic;
using System.Text;
using Amplifir.Core.Enums;
using Amplifir.Core.Entities;

namespace Amplifir.Core.Models
{
    public class CreateShoutResult
    {
        public CreateShoutState State { get; set; }

        public Shout NewShout { get; set }
    }
}
