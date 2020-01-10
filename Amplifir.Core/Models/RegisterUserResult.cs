using Amplifir.Core.Enums;
using Amplifir.Core.Interfaces;

namespace Amplifir.Core.Models
{
    public class RegisterUserResult
    {
        public RegisterUserState State { get; set; }

        public IAppUser User { get; set; }
    }
}
