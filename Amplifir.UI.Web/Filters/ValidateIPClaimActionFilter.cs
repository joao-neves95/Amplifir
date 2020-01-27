/*
 * Copyright (c) 2019 - 2020 João Pedro Martins Neves (SHIVAYL) - All Rights Reserved.
 *
 * Amplifir and all its content is licensed under the GNU Lesser General Public License (LGPL),
 * version 3, located in the root of this project, under the name "LICENSE.md".
 *
 */

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Amplifir.Core.Interfaces;
using Amplifir.UI.Web.Utilities;

namespace Amplifir.UI.Web.Filters
{
    public class ValidateIPClaimActionFilter : IAuthorizationFilter
    {
        public ValidateIPClaimActionFilter(IJWTService jWTService)
        {
            this._JWTService = jWTService;
        }

        private readonly IJWTService _JWTService;

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            if (!this._JWTService.ValidateJWTUserIp( context.HttpContext.User, HttpUtils.GetUserIp( context.HttpContext ) ))
            {
                context.Result = new UnauthorizedResult();
            }
        }
    }
}
