using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Amplifir.Core.Interfaces;
using Amplifir.Core.DTOs;
using Amplifir.Core.Entities;
using Amplifir.Core.Models;
using Amplifir.Core.Enums;
using Amplifir.Core.Utilities;
using Amplifir.UI.Web.Resources;

// TODO: Before production, remove every API exception returns.

namespace Amplifir.UI.Web.Controllers
{
    [Route("api/shouts")]
    [ApiController]
    [System.Diagnostics.CodeAnalysis.SuppressMessage( "Reliability", "CA2007:Consider calling ConfigureAwait on the awaited task", Justification = "<Pending>" )]
    public class ShoutsController : ControllerBase
    {
        public ShoutsController(IShoutService shoutService)
        {
            this._shoutService = shoutService;
        }

        #region PROPERTIES

        private readonly IShoutService _shoutService;

        #endregion

        #region PUBLIC ENDPOINTS

        #region GET

        /// <summary>
        /// 
        /// Gets shouts paginated.
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                return Ok( new ApiResponse<string>()
                {
                    EndpointResult = "Get"
                } );
            }
            catch (Exception e)
            {
                return Problem( statusCode: 500, detail: Newtonsoft.Json.JsonConvert.SerializeObject( e, Newtonsoft.Json.Formatting.Indented ) );
            }
        }

        /// <summary>
        /// 
        /// Gets a specific shout by id.
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet( "{id}" )]
        public async Task<IActionResult> GetById([FromRoute]int id)
        {
            try
            {
                return Ok( new ApiResponse<string>()
                {
                    EndpointResult = "GetById"
                } );
            }
            catch (Exception e)
            {
                return Problem( statusCode: 500, detail: Newtonsoft.Json.JsonConvert.SerializeObject( e, Newtonsoft.Json.Formatting.Indented ) );
            }
        }

        /// <summary>
        /// 
        /// Gets all shouts paginated of a specific user id.
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet( "user/{userId}" )]
        public async Task<IActionResult> GetByUserId([FromRoute]int userId)
        {
            try
            {
                return Ok( new ApiResponse<string>()
                {
                    EndpointResult = "GetByUserId"
                } );
            }
            catch (Exception e)
            {
                return Problem( statusCode: 500, detail: Newtonsoft.Json.JsonConvert.SerializeObject( e, Newtonsoft.Json.Formatting.Indented ) );
            }
        }

        #endregion GET

        #region POST

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Post([FromBody]NewShoutDTO newShoutDTO)
        {
            ApiResponse<CreateShoutResult> apiResponse = new ApiResponse<CreateShoutResult>();

            try
            {
                CreateShoutResult createShoutResult = await this._shoutService.CreateAsync( newShoutDTO as Shout );

                if (createShoutResult.State != CreateShoutState.Success)
                {
                    apiResponse.Error = true;
                    apiResponse.Message = createShoutResult.State.Switch( new Dictionary<CreateShoutState, Func<string>>()
                    {
                        { CreateShoutState.ContentTooLong, () => Resource_ResponseMessages_en.ContentTooLong },
                        { CreateShoutState.ContentTooSmall, () => Resource_ResponseMessages_en.ContentTooSmall }
                    },
                        () => Resource_ResponseMessages_en.Unknown
                    );

                    return BadRequest( apiResponse );
                }

                return Ok( new ApiResponse<CreateShoutResult>()
                {
                    EndpointResult = createShoutResult
                } );
            }
            catch (Exception e)
            {
                return Problem( statusCode: 500, detail: Newtonsoft.Json.JsonConvert.SerializeObject( e, Newtonsoft.Json.Formatting.Indented ) );
            }
        }

        #endregion POST

        #endregion PUBLIC ENDPOINTS
    }
}
