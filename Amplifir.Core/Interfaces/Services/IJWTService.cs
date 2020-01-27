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
using System.Security.Claims;

namespace Amplifir.Core.Interfaces
{
    public interface IJWTService
    {
        string Generate( IAppUser appUser );

        bool ValidateJWTUserIp( ClaimsPrincipal userClaims, string userIp );

        /// <summary>
        /// 
        /// Get a value of a claim from the user's request JWT.
        /// 
        /// </summary>
        /// <param name="userClaims"> The User object from context. </param>
        /// <param name="claimType"> A ClaimType value or a custom claim. </param>
        /// <returns></returns>
        string GetClaim( ClaimsPrincipal userClaims, string claimType );

        /// <summary>
        /// 
        /// Get the ID of an user from the JWT claims.
        /// 
        /// </summary>
        /// <param name="userClaims"> The User object from context. </param>
        /// <returns></returns>
        string GetClaimId( ClaimsPrincipal userClaims );

        /// <summary>
        /// 
        /// Get the IPv4 of a user from the JWT claims.
        /// 
        /// </summary>
        /// <param name="userClaims"></param>
        /// <returns></returns>
        string GetClaimIPv4( ClaimsPrincipal userClaims );
    }
}
