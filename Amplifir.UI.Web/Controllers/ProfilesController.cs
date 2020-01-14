/*
 * Copyright (c) 2019 - 2020 João Pedro Martins Neves (SHIVAYL) - All Rights Reserved.
 *
 * Amplifir and all its content is licensed under the GNU Lesser General Public License (LGPL),
 * version 3, located in the root of this project, under the name "LICENSE.md".
 *
 */

using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Amplifir.Core.Interfaces;
using Amplifir.Core.Entities;
using Amplifir.Core.DTOs;

namespace Amplifir.UI.Web.Controllers
{
    [Route("api/profiles")]
    [ApiController]
    public class ProfilesController : ControllerBase
    {
        public ProfilesController(IUserProfileService userProfileService, IJWTService jWTService)
        {
            this._userProfileService = userProfileService;
            this._jWTService = jWTService;
        }

        #region PROPERTIES

        private readonly IUserProfileService _userProfileService;

        private readonly IJWTService _jWTService;

        #endregion

        /// <summary>
        /// 
        /// <para>
        ///   Gets the requested user profile.
        ///   Sending -1 as the id, gets the current authenticated user.
        /// </para>
        /// 
        /// <code>
        ///   GET: [controller-route]/69
        /// </code>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}", Name = "Get")]
        [Authorize]
        public async Task<IActionResult> Get([FromRoute]int id)
        {
            try
            {
                if (id == -1)
                {
                    // If FormatException Exception occurs, let the catch handle.
                    id = Convert.ToInt32( _jWTService.GetClaimId( HttpContext.User ) );
                }

                return Ok( new ApiResponse<AppUserProfile>()
                {
                    EndpointResult = await _userProfileService.GetByUserIdAsync(id)
                } );
            }
            catch (Exception e)
            {
                return Problem( statusCode: 500, detail: Newtonsoft.Json.JsonConvert.SerializeObject( e, Newtonsoft.Json.Formatting.Indented ) );
            }
        }

        /// <summary>
        /// 
        /// <para>
        ///   Updates the complete profile of the current authenticated user.
        /// </para>
        /// 
        /// <code>
        ///   PUT: [controller-route]
        /// </code>
        /// 
        /// </summary>
        [HttpPut]
        [Authorize]
        public void Put()
        {
            // Get the claim id from the JWT auth token.
        }

        /// <summary>
        /// 
        /// <para>
        ///   Deletes the profile of the current authenticated user.
        /// </para>
        /// 
        /// <code>
        ///   DELETE: [controller-route]
        /// </code>
        /// 
        /// </summary>
        [HttpDelete]
        public void Delete()
        {
            // Get the claim id from the JWT auth token.
        }
    }
}
