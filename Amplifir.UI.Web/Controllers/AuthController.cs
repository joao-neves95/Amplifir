using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Data.Common;
using Amplifir.Core.Interfaces;
using Amplifir.Core.DTOs;
using Amplifir.Core.Enums;
using Amplifir.Core.Models;
using Amplifir.Core.Entities;
using Amplifir.UI.Web.Utilities;

namespace Amplifir.UI.Web.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        public AuthController(IAuthenticationService authenticationService, IJWTService jWTService)
        {
            this._authenticationService = authenticationService;
            this._jWTService = jWTService;
        }

        #region PROPERTIES

        private readonly IAuthenticationService _authenticationService;

        private readonly IJWTService _jWTService;

        #endregion PROPERTIES

        #region METHODS

        [HttpPost( "register" )]
        public async Task<IActionResult> Register([FromBody]UserCredentialsDTO userCredentialsDTO)
        {
            try
            {
                RegisterUserResult registerUserResult = await this._authenticationService.RegisterUserAsync( new AppUser()
                {
                    Email = userCredentialsDTO.Email,
                    Password = userCredentialsDTO.Password,
                    Ipv4 = HttpUtils.GetUserIp( HttpContext )
                } );

                if (registerUserResult.State != RegisterUserState.Success)
                {
                    // TODO: Error handling.
                    return BadRequest();
                }
                else
                {
                    return RedirectToActionPermanent( "login", new UserCredentialsDTO( registerUserResult.User.Email, registerUserResult.User.Password ) );
                }
            }
            catch (DbException e)
            {
                // TODO: Error handling.
                return Problem( statusCode: 500, detail: Newtonsoft.Json.JsonConvert.SerializeObject( e, Newtonsoft.Json.Formatting.Indented ) );
            }
            catch (Exception e)
            {
                // TODO: Error handling.
                return Problem( statusCode: 500, detail: Newtonsoft.Json.JsonConvert.SerializeObject( e, Newtonsoft.Json.Formatting.Indented ) );
            }
        }

        [HttpPost( "login" )]
        public async Task<IActionResult> Login([FromBody]UserCredentialsDTO userCredentialsDTO)
        {
            try
            {
                ValidateSignInResult validateSignInResult = await this._authenticationService.ValidateSignInAsync( userCredentialsDTO.Email, userCredentialsDTO.Password );

                if (validateSignInResult.State != ValidateSignInState.Success)
                {
                    // TODO: Error handling.
                    return BadRequest();
                }
                else
                {
                    validateSignInResult.User.Ipv4 = HttpUtils.GetUserIp( HttpContext );
                    return Ok( new LoginResponse( this._jWTService.Generate( validateSignInResult.User ) ) );
                }
            }
            catch (DbException e)
            {
                // TODO: Error handling.
                return Problem( statusCode: 500, detail: Newtonsoft.Json.JsonConvert.SerializeObject( e, Newtonsoft.Json.Formatting.Indented ) );
            }
            catch (Exception e)
            {
                // TODO: Error handling.
                return Problem( statusCode: 500, detail: Newtonsoft.Json.JsonConvert.SerializeObject( e, Newtonsoft.Json.Formatting.Indented ) );
            }
        }

        [HttpPost( "logout" )]
        public void Logout() { }

        #endregion METHODS
    }
}
