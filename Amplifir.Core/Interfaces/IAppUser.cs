using System;
using System.Collections.Generic;
using System.Text;

namespace Amplifir.Core.Interfaces
{
    /// <summary>
    /// 
    /// Interface to interact with user data: Id, Email, Password.
    /// 
    /// </summary>
    public interface IAppUser
    {
        int Id { get; set; }

        string Email { get; set; }

        string Password { get; set; }
    }
}
