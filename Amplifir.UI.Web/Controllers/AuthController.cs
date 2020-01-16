/*
 * Copyright (c) 2019 - 2020 João Pedro Martins Neves (SHIVAYL) - All Rights Reserved.
 *
 * Amplifir and all its content is licensed under the GNU Lesser General Public License (LGPL),
 * version 3, located in the root of this project, under the name "LICENSE.md".
 *
 */

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Data.Common;
using Microsoft.AspNetCore.Mvc;
using Amplifir.Core.Enums;
using Amplifir.Core.Interfaces;
using Amplifir.Core.DTOs;
using Amplifir.Core.Models;
using Amplifir.Core.Entities;
using Amplifir.Core.Utilities;
using Amplifir.UI.Web.Utilities;
using Amplifir.UI.Web.Resources;

// TODO: Before production, remove every API exception returns.

namespace Amplifir.UI.Web.Controllers
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage( "Reliability", "CA2007:Consider calling ConfigureAwait on the awaited task", Justification = "<Pending>" )]
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

        #region PUBLIC ENDPOINTS

        [HttpPost( "register" )]
        [Produces( typeof( ApiResponse<LoginResponse> ) ) ]
        public async Task<IActionResult> Register([FromBody]UserCredentialsDTO userCredentialsDTO)
        {
            ApiResponse<LoginResponse> apiResponse = new ApiResponse<LoginResponse>();

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
                    apiResponse.Error = true;
                    apiResponse.Message = registerUserResult.State.Switch( new Dictionary<RegisterUserState, Func<string>>()
                    {
                        { RegisterUserState.EmailExists, () => Resource_ResponseMessages_en.EmailExists },
                        { RegisterUserState.PasswordTooSmall, () => Resource_ResponseMessages_en.PasswordTooSmall },
                        { RegisterUserState.InvalidEmail, () => Resource_ResponseMessages_en.InvalidEmail }
                    },
                        () => Resource_ResponseMessages_en.Unknown
                    );

                    return BadRequest( apiResponse );
                }

                apiResponse.Message = Resource_ResponseMessages_en.RegisterSuccess;
                apiResponse.EndpointResult = new LoginResponse( this._jWTService.Generate( newAppUser ) );

                return Ok( apiResponse );
            }
            catch (DbException e)
            {
                apiResponse.Error = true;
                apiResponse.Message = Resource_ResponseMessages_en.Unknown;
                // TODO: Error handling.
                return Problem( statusCode: 500, detail: Newtonsoft.Json.JsonConvert.SerializeObject( e, Newtonsoft.Json.Formatting.Indented ) );
            }
            catch (Exception e)
            {
                apiResponse.Error = true;
                apiResponse.Message = Resource_ResponseMessages_en.Unknown;
                // TODO: Error handling.
                return Problem( statusCode: 500, detail: Newtonsoft.Json.JsonConvert.SerializeObject( e, Newtonsoft.Json.Formatting.Indented ) );
            }
        }

        [HttpPost( "login" )]
        [Produces( typeof( ApiResponse<LoginResponse> ) )]
        public async Task<IActionResult> Login([FromBody]UserCredentialsDTO userCredentialsDTO)
        {
            ApiResponse<LoginResponse> apiResponse = new ApiResponse<LoginResponse>();

            try
            {
                ValidateSignInResult validateSignInResult = await this._authenticationService.ValidateSignInAsync( userCredentialsDTO.Email, userCredentialsDTO.Password );

                if (validateSignInResult.State != ValidateSignInState.Success)
                {
                    apiResponse.Error = true;
                    apiResponse.Message = validateSignInResult.State.Switch( new Dictionary<ValidateSignInState, Func<string>>()
                    {
                        { ValidateSignInState.NotFound, () => Resource_ResponseMessages_en.UserNotFound  },
                        { ValidateSignInState.WrongPassword, () => Resource_ResponseMessages_en.WrongPassword  }
                    },
                        () => Resource_ResponseMessages_en.Unknown
                    );

                    return BadRequest( apiResponse );
                }

                validateSignInResult.User.Ipv4 = HttpUtils.GetUserIp( HttpContext );

                // Do not await in order to respond as fast as possible,
                // the task completion is not necessary for the response creation.
                _ = _auditLogStore.CreateLogAsync( new AuditLog()
                {
                    UserId = validateSignInResult.User.Id,
                    EventTypeId = EventTypeId.Login,
                    IPv4 = validateSignInResult.User.Ipv4
                } );

                apiResponse.Message = Resource_ResponseMessages_en.LoginSuccess;
                apiResponse.EndpointResult = new LoginResponse( this._jWTService.Generate( validateSignInResult.User ) );

                return Ok( apiResponse );
            }
            catch (DbException e)
            {
                apiResponse.Error = true;
                apiResponse.Message = Resource_ResponseMessages_en.Unknown;
                // TODO: Error handling.
                return Problem( statusCode: 500, detail: Newtonsoft.Json.JsonConvert.SerializeObject( e, Newtonsoft.Json.Formatting.Indented ) );
            }
            catch (Exception e)
            {
                apiResponse.Error = true;
                apiResponse.Message = Resource_ResponseMessages_en.Unknown;
                // TODO: Error handling.
                return Problem( statusCode: 500, detail: Newtonsoft.Json.JsonConvert.SerializeObject( e, Newtonsoft.Json.Formatting.Indented ) );
            }
        }

        #endregion PUBLIC ENDPOINTS
    }
}
