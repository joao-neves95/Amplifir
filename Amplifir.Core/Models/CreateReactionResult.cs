using Amplifir.Core.Enums;

namespace Amplifir.Core.Models
{
    public class CreateReactionResult<TReaction>
    {
        public CreateReactionState State { get; set; }

        public EntityType EntityType { get; set; }

        public int EntityId { get; set; }

        public TReaction Reaction { get; set; }
    }
}
