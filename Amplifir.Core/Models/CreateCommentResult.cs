using Amplifir.Core.Entities;
using Amplifir.Core.Enums;

namespace Amplifir.Core.Models
{
    public class CreateCommentResult
    {
        public CreateShoutState State { get; set; }

        public Comment NewComment { get; set; }
    }
}
