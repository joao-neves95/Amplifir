using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Amplifir.Core.Enums;

namespace Amplifir.Core.Interfaces
{
    /// <summary>
    /// 
    /// Service for authentication and authorization.
    /// 
    /// </summary>
    public interface IAuthenticationService
    {
        Task<RegisterUserResult> RegisterUserAsync( string email, string password );

        Task<ValidateSignInResult> ValidateSignInAsync( string email, string password );
    }
}
