using System;
using System.Collections.Generic;
using System.Text;

namespace Amplifir.Core.Enums
{
    public enum ValidateSignInState
    {
        Success,
        NotFound,
        InvalidEmail,
        InvalidPassword,
        TwoFactorRequired,
        LockedOut,
        Unknown
    }
}
