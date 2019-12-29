using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Amplifir.Core.Interfaces;
using Amplifir.Core.DTOs;

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

        [HttpPost]
        public void Register([FromBody]UserCredentialsDTO userCredentialsDTO) 
        {
        }

        [HttpPost]
        public void Login([FromBody]UserCredentialsDTO userCredentialsDTO)
        {
        }

        public void Logout() { }
    }
}
