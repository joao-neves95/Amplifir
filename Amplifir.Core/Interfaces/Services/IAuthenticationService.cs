/*
 * Copyright (c) 2019 - 2020 João Pedro Martins Neves (SHIVAYL) - All Rights Reserved.
 *
 * Amplifir and all its content is licensed under the GNU Lesser General Public License (LGPL),
 * version 3, located in the root of this project, under the name "LICENSE.md".
 *
 */

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
