using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Amplifir.Core.Interfaces
{
    /// <summary>
    /// 
    /// Service for authentication and authorization.
    /// 
    /// </summary>
    public interface IAuthenticationService
    {
        Task RegisterUser( string email, string password );

        Task LoginUser( string email, string password );
    }
}
