using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Amplifir.Core.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Amplifir.UI.Web.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        public AuthController(IAuthenticationService authenticationService)
        {
            this._authenticationService = authenticationService;
        }

        private readonly IAuthenticationService _authenticationService;

        public void Register() { }

        public void Login() { }

        public void Logout() { }
    }
}
