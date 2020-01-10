using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Amplifir.Core.Enums;
using Amplifir.Core.Entities;
using Amplifir.Core.Models;

namespace Amplifir.Core.Interfaces
{
    /// <summary>
    /// 
    /// Service for authentication and authorization.
    /// 
    /// </summary>
    public interface IAuthenticationService
    {
        Task<RegisterUserResult> RegisterUserAsync( IAppUser appUser );

        Task<ValidateSignInResult> ValidateSignInAsync( string email, string password );
    }
}
