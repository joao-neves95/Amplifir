/*
 * Copyright (c) 2019 - 2020 João Pedro Martins Neves (SHIVAYL) - All Rights Reserved.
 *
 * Amplifir and all its content is licensed under the GNU Lesser General Public License (LGPL),
 * version 3, located in the root of this project, under the name "LICENSE.md".
 *
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Amplifir.UI.Web.Utilities
{
    public class HttpUtils
    {
        public static string GetUserIp( HttpContext httpContext )
        {
            if (!String.IsNullOrEmpty( httpContext.Request.Headers["X-Forwarded-For"] ) )
            {
                // It's possible that it comes multiple addresses.
                return httpContext.Request.Headers["X-Forwarded-For"].ToString().Split( ',' )[0];
            }
            else if (!String.IsNullOrEmpty( httpContext.Request.Headers["CF-Connecting-IP"] ))
            {
                return httpContext.Request.Headers["CF-Connecting-IP"].ToString().Split( ',' )[0];
            }
            else
            {
                return httpContext.Connection.RemoteIpAddress.MapToIPv4().ToString();
            }
        }
    }
}
