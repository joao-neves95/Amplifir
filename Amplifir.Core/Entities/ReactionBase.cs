using Amplifir.Core.Interfaces;

namespace Amplifir.Core.Entities
{
    public abstract class ReactionBase : IReaction
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public short ReactionTypeId { get; set; }
    }
}
