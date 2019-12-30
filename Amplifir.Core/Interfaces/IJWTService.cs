using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Claims;

namespace Amplifir.Core.Interfaces
{
    public interface IJWTService
    {
        string Generate( string email, string userId );

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
    }
}
