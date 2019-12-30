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
        public string Generate( int userId )
        {
            JwtSecurityToken token = new JwtSecurityToken(
                DotNetEnv.Env.GetString( "JWT_ISSUER" ),
                DotNetEnv.Env.GetString( "JWT_ISSUER" ),

                new List<Claim>()
                {
                    // https://tools.ietf.org/html/rfc7519#section-4
                    // Sub = Subject.
                    new Claim( JwtRegisteredClaimNames.Sub, userId.ToString() ),
                    // Iat = Issued at.
                    new Claim( JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString(), ClaimValueTypes.DateTime ),
                    new Claim( "id", userId.ToString() ),
                },

                notBefore: DateTime.UtcNow,
                expires: DateTime.UtcNow.AddDays( DotNetEnv.Env.GetInt( "JWT_EXPIRATION_DAYS" ) ),
                signingCredentials: new SigningCredentials( new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes( DotNetEnv.Env.GetString( "JWT_KEY" ) ) ),
                    SecurityAlgorithms.HmacSha512
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
