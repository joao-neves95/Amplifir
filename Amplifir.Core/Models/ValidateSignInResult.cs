using Amplifir.Core.Enums;
using Amplifir.Core.Interfaces;

namespace Amplifir.Core.Models
{
    public class ValidateSignInResult
    {
        public ValidateSignInState State { get; set; }

        public IAppUser User { get; set; }
    }
}
