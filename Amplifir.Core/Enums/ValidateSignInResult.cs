using System;
using System.Collections.Generic;
using System.Text;

namespace Amplifir.Core.Enums
{
    public enum ValidateSignInResult
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
