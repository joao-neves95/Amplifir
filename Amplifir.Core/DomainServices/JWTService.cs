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
using System.Linq;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using Amplifir.Core.Interfaces;

namespace Amplifir.Core.DomainServices
{
    public class JWTService : IJWTService
    {
        public JWTService(IAppSecrets appSecrets)
        {
            this._appSecrets = appSecrets;
        }

        private readonly IAppSecrets _appSecrets;

        public string Generate( IAppUser appUser )
        {
            JwtSecurityToken token = new JwtSecurityToken(
                _appSecrets.JWT_Issuer,
                _appSecrets.JWT_Issuer,

                new List<Claim>()
                {
                    // https://tools.ietf.org/html/rfc7519#section-4
                    // Sub = Subject.
                    new Claim( JwtRegisteredClaimNames.Sub, appUser.Id.ToString() ),
                    // Iat = Issued at.
                    new Claim( JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString(), ClaimValueTypes.DateTime ),
                    new Claim( "id", appUser.Id.ToString() ),
                    new Claim( "ipv4", appUser.Ipv4 )
                },

                notBefore: DateTime.UtcNow,
                expires: DateTime.UtcNow.AddDays( _appSecrets.JWT_ExpirationDays ),
                signingCredentials: new SigningCredentials( new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes( _appSecrets.JWT_Key ) ),
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

        public string GetClaimIPv4(ClaimsPrincipal userClaims)
        {
            return GetClaim( userClaims, "ipv4" );
        }
    }
}
