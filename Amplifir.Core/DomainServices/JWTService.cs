using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Amplifir.Core.Interfaces;
using Microsoft.IdentityModel.Tokens;

namespace Amplifir.Core.DomainServices
{
    public class JWTService : IJWTService
    {
        // TODO: Test DotNetEnv.Env outside the Web project.
        // If null, pass it as a method paramenter.
        public string Generate( string email, string userId )
        {
            JwtSecurityToken token = new JwtSecurityToken(
                DotNetEnv.Env.GetString( "JWT_ISSUER" ),
                DotNetEnv.Env.GetString( "JWT_ISSUER" ),

                new List<Claim>()
                {
                    // https://tools.ietf.org/html/rfc7519#section-4
                    // Sub = Subject.
                    new Claim( JwtRegisteredClaimNames.Sub, email ),
                    // Iat = Issued at.
                    new Claim( JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString() ),
                    new Claim( ClaimTypes.NameIdentifier, email ),
                    new Claim( ClaimTypes.Name, email ),
                    new Claim( ClaimTypes.Email, email ),
                    new Claim( "id", userId ),
                },

                expires: DateTime.UtcNow.AddDays( 7 ),
                signingCredentials: new SigningCredentials( new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes( DotNetEnv.Env.GetString( "JWT_KEY" ) ) ),
                    SecurityAlgorithms.RsaSha512
                 )
            );

            return new JwtSecurityTokenHandler().WriteToken( token );
        }

        public string GetClaim(ClaimsPrincipal userClaims, string claimType)
        {
            if (userClaims == null)
            {
                return null;
            }

            return userClaims.Claims.Where( c => c.Type == claimType ).First().Value;
        }

        public string GetClaimId(ClaimsPrincipal userClaims)
        {
            return GetClaim( userClaims, "id" );
        }
    }
}
