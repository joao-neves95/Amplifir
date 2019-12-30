using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Amplifir.Core.Interfaces;
using Amplifir.Core.DTOs;
using Amplifir.Core.Enums;
using Amplifir.Core.Models;

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
                RegisterUserResult registerUserResult = await this._authenticationService.RegisterUserAsync( userCredentialsDTO.Email, userCredentialsDTO.Password );

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
            catch (Exception e)
            {
                // TODO: Error handling.
                return Problem( detail: "Unknown Error", statusCode: 500 );
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
                    return Ok( new LoginResponse( this._jWTService.Generate( validateSignInResult.User.Id ) ) );
                }
            }
            catch (Exception e)
            {
                // TODO: Error handling.
                return Problem( statusCode: 500, detail: "Unknown Error" );
            }
        }

        [HttpPost( "logout" )]
        public void Logout() { }

        #endregion METHODS
    }
}
