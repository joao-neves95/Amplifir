using Amplifir.Core.Enums;

namespace Amplifir.Core.Models
{
    public class CreateReactionResult
    {
        public CreateReactionState State { get; set; }

        public EntityType EntityType { get; set; }

        public int ReactionId { get; set; }
    }
}
