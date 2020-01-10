using System;
using System.Collections.Generic;
using System.Text;

namespace Amplifir.Core.Interfaces
{
    public interface IReaction
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public short ReactionTypeId { get; set; }
    }
}
