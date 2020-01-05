using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Amplifir.Core.DTOs;

namespace Amplifir.UI.Web.Controllers
{
    [Route("api/shouts")]
    [ApiController]
    public class ShoutsController : ControllerBase
    {
        public ShoutsController()
        {
        }

        #region PROPERTIES
        #endregion

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
    }
}