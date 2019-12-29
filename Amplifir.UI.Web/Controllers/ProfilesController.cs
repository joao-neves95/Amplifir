using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace Amplifir.UI.Web.Controllers
{
    [Route("api/profiles")]
    [ApiController]
    public class ProfilesController : ControllerBase
    {
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
        public string Get([FromRoute]int id)
        {
            // Get the claim id from the JWT auth token, if the id == -1.
            return "value";
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
