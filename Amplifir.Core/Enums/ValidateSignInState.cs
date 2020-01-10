using System;
using System.Collections.Generic;
using System.Text;

namespace Amplifir.Core.Enums
{
    public enum ValidateSignInState
    {
        Success,
        NotFound,
        WrongPassword,
        TwoFactorRequired,
        LockedOut,
        Unknown
    }
}
