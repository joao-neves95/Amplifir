using System;
using System.Collections.Generic;
using System.Text;

namespace Amplifir.Core.Enums
{
    public enum RegisterUserState
    {
        Success,
        PasswordTooSmall,
        EmailExists,
        InvalidEmail,
        Unknown
    }
}
