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
