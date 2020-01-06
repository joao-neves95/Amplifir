using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Data.Common;
using Microsoft.AspNetCore.Mvc;
using Amplifir.Core.Interfaces;
using Amplifir.Core.DTOs;
using Amplifir.Core.Enums;
using Amplifir.Core.Models;
using Amplifir.Core.Entities;
using Amplifir.Core.Utilities;
using Amplifir.UI.Web.Utilities;
using Amplifir.UI.Web.Resources;

// TODO: Before production, remove every API exception returns.

namespace Amplifir.UI.Web.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        public AuthController(IAuthenticationService authenticationService, IJWTService jWTService, IAuditLogStore auditLogStore)
        {
            this._authenticationService = authenticationService;
            this._jWTService = jWTService;
            this._auditLogStore = auditLogStore;
        }

        #region PROPERTIES

        private readonly IAuthenticationService _authenticationService;

        private readonly IJWTService _jWTService;

        private readonly IAuditLogStore _auditLogStore;

        #endregion PROPERTIES

        #region ENDPOINT METHODS

        [HttpPost( "register" )]
        [System.Diagnostics.CodeAnalysis.SuppressMessage( "Reliability", "CA2007:Consider calling ConfigureAwait on the awaited task", Justification = "<Pending>" )]
        public async Task<IActionResult> Register([FromBody]UserCredentialsDTO userCredentialsDTO)
        {
            try
            {
                IAppUser newAppUser = new AppUser()
                {
                    Email = userCredentialsDTO.Email,
                    Password = userCredentialsDTO.Password,
                    Ipv4 = HttpUtils.GetUserIp( HttpContext )
                };

                RegisterUserResult registerUserResult = await this._authenticationService.RegisterUserAsync( newAppUser );

                if (registerUserResult.State != RegisterUserState.Success)
                {
                    // TODO: Error handling.
                    return BadRequest( new ApiResponse<bool>()
                    {
                        Error = true,
                        Message = registerUserResult.State.Switch( new Dictionary<RegisterUserState, Func<string>>()
                        {
                            { RegisterUserState.EmailExists, () => Resource_ResponseMessages_en.EmailExists },
                            { RegisterUserState.PasswordTooSmall, () => Resource_ResponseMessages_en.PasswordTooSmall },
                            { RegisterUserState.InvalidEmail, () => Resource_ResponseMessages_en.InvalidEmail }
                        },
                            () => Resource_ResponseMessages_en.Unknown
                        )
                    } );
                }
                else
                {
                    return Ok( new ApiResponse<LoginResponse>()
                    {
                        Message = Resource_ResponseMessages_en.RegisterSuccess,
                        EndpointResult = new LoginResponse( this._jWTService.Generate( newAppUser ) )
                    } );
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
                    return BadRequest( new ApiResponse<bool>()
                    {
                        Error = true,
                        Message = validateSignInResult.State.Switch( new Dictionary<ValidateSignInState, Func<string>>()
                        {
                            { ValidateSignInState.NotFound, () => Resource_ResponseMessages_en.UserNotFound  },
                            { ValidateSignInState.WrongPassword, () => Resource_ResponseMessages_en.WrongPassword  }
                        },
                            () => Resource_ResponseMessages_en.Unknown
                        )
                    } );
                }
                else
                {
                    validateSignInResult.User.Ipv4 = HttpUtils.GetUserIp( HttpContext );

                    // Do not await in order to respond as fast as possible,
                    // the task completion is not necessary for the response creation.
                    _ = _auditLogStore.CreateLog( new AuditLog()
                    {
                        UserId = validateSignInResult.User.Id,
                        EventTypeId = EventTypeId.Login,
                        IPv4 = validateSignInResult.User.Ipv4
                    } );

                    return Ok( new ApiResponse<LoginResponse>()
                    {
                        Message = Resource_ResponseMessages_en.LoginSuccess,
                        EndpointResult = new LoginResponse( this._jWTService.Generate( validateSignInResult.User ) )
                    } );
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

        #endregion ENDPOINT METHODS
    }
}
