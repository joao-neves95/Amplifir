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
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data.Common;
using Amplifir.Core.Interfaces;
using Amplifir.Core.DTOs;
using Amplifir.Core.Entities;
using Amplifir.Core.Models;
using Amplifir.Core.Enums;
using Amplifir.Core.Utilities;
using Amplifir.UI.Web.Resources;
using Amplifir.UI.Web.Filters;

// TODO: Before production, remove every API exception returns.

namespace Amplifir.UI.Web.Controllers
{
    [Route("api/shouts")]
    [ApiController]
    [System.Diagnostics.CodeAnalysis.SuppressMessage( "Reliability", "CA2007:Consider calling ConfigureAwait on the awaited task", Justification = "<Pending>" )]
    [System.Diagnostics.CodeAnalysis.SuppressMessage( "Globalization", "CA1305:Specify IFormatProvider", Justification = "<Pending>" )]
    public class ShoutsController : ControllerBase
    {
        public ShoutsController(IShoutService shoutService, IJWTService jWTService)
        {
            this._shoutService = shoutService;
            this._JWTService = jWTService;
        }

        #region PROPERTIES

        private readonly IShoutService _shoutService;

        private readonly IJWTService _JWTService;

        #endregion

        #region PUBLIC ENDPOINTS

        #region GET

        /// <summary>
        /// 
        /// Gets top shouts paginated.
        ///
        /// ?filterBy=1 amp; lastId=0 amp; limit=10
        /// 
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Produces( typeof( ApiResponse<List<Shout>> ) )]
        public async Task<IActionResult> Get([FromQuery]FilterType filterBy = FilterType.Top, [FromQuery]int lastId = 0, [FromQuery]short limit = 10)
        {
            ApiResponse<List<Shout>> apiResponse = new ApiResponse<List<Shout>>();

            try
            {
                apiResponse.EndpointResult = await this._shoutService.GetAsync( new ShoutsFilter( filterBy ), lastId, limit );
                return Ok( apiResponse );
            }
            catch (ArgumentOutOfRangeException e)
            {
                apiResponse.Error = true;
                apiResponse.Message = Resource_ResponseMessages_en.BadRequest;
                return BadRequest( apiResponse );
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
        /// <para> Gets all shouts paginated of a specific user id. </para>
        /// <para> If the user is -1, it defaults to the current user in session. </para>
        /// <para> GET: " user/{userId} ? lastId={lastId::0} &amp; limit={limit::10} " </para>
        /// 
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="lastId"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        [HttpGet( "user/{userId}" )]
        [Produces( typeof( ApiResponse<List<Shout>> ) )]
        public async Task<IActionResult> GetByUserId([FromRoute]int userId, [FromQuery]int lastId = 0, [FromQuery]short limit = 10)
        {
            ApiResponse<List<Shout>> apiResponse = new ApiResponse<List<Shout>>();

            try
            {
                if (userId == -1)
                {
                    bool parsed = int.TryParse( this._JWTService.GetClaimId( HttpContext.User ), out userId );

                    // The user is not authenticated (guest).
                    if (!parsed)
                    {
                        apiResponse.EndpointResult = new List<Shout>( 0 );
                        return Ok( apiResponse );
                    }
                }

                apiResponse.EndpointResult = await this._shoutService.GetByUserIdAsync( userId, lastId, limit );
                return Ok( apiResponse );
            }
            catch (FormatException e)
            {
                apiResponse.Error = true;
                apiResponse.Message = Resource_ResponseMessages_en.BadRequest;
                return BadRequest( apiResponse );
            }
            catch (ArgumentOutOfRangeException e)
            {
                apiResponse.Error = true;
                apiResponse.Message = Resource_ResponseMessages_en.BadRequest;
                return BadRequest( apiResponse );
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
                return Problem( statusCode: 500, detail: Newtonsoft.Json.JsonConvert.SerializeObject( e, Newtonsoft.Json.Formatting.Indented ) );
            }
        }

        [HttpGet( "{shoutId}/comments" )]
        [Produces( typeof( ApiResponse<List<Comment>> ) )]
        public async Task<IActionResult> GetComments([FromRoute]int shoutId, [FromQuery]int lastId = 0, [FromQuery]short limit = 10)
        {
            ApiResponse<List<Comment>> apiResponse = new ApiResponse<List<Comment>>();

            try
            {
                apiResponse.EndpointResult = await this._shoutService.GetCommentsByShoutIdAsync( shoutId, lastId, limit );
                return Ok( apiResponse );
            }
            catch (ArgumentOutOfRangeException e)
            {
                apiResponse.Error = true;
                apiResponse.Message = Resource_ResponseMessages_en.BadRequest;
                return BadRequest( apiResponse );
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
                return Problem( statusCode: 500, detail: Newtonsoft.Json.JsonConvert.SerializeObject( e, Newtonsoft.Json.Formatting.Indented ) );
            }
        }

        #endregion GET

        #region POST

        [HttpPost]
        [Authorize]
        [TypeFilter( typeof( ValidateIPClaimActionFilter ) )]
        [Produces( typeof( ApiResponse<CreateShoutResult> ) )]
        public async Task<IActionResult> Post([FromBody]NewShoutDTO newShoutDTO)
        {
            ApiResponse<CreateShoutResult> apiResponse = new ApiResponse<CreateShoutResult>();

            try
            {
                newShoutDTO.UserId = Convert.ToInt32( this._JWTService.GetClaimId( HttpContext.User ) );
                apiResponse.EndpointResult = await this._shoutService.CreateAsync( newShoutDTO.ToShout() );

                if (apiResponse.EndpointResult.State != CreateShoutState.Success)
                {
                    apiResponse.Error = true;
                    apiResponse.Message = apiResponse.EndpointResult.State.Switch( new Dictionary<CreateShoutState, Func<string>>()
                    {
                        { CreateShoutState.ContentTooLong, () => Resource_ResponseMessages_en.ContentTooLong },
                        { CreateShoutState.ContentTooSmall, () => Resource_ResponseMessages_en.ContentTooSmall }
                    },
                        () => Resource_ResponseMessages_en.Unknown
                    );

                    return BadRequest( apiResponse );
                }

                apiResponse.Message = Resource_ResponseMessages_en.CreateShoutSuccess;

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
                return Problem( statusCode: 500, detail: Newtonsoft.Json.JsonConvert.SerializeObject( e, Newtonsoft.Json.Formatting.Indented ) );
            }
        }

        [HttpPost("{shoutId}/likes")]
        [Authorize]
        [TypeFilter( typeof( ValidateIPClaimActionFilter ) )]
        [Produces( typeof( ApiResponse<CreateReactionResult> ) )]
        public async Task<IActionResult> PostLike([FromRoute]int shoutId)
        {
            return await this.PostReaction( shoutId, true, true );
        }

        [HttpPost( "{shoutId}/dislikes" )]
        [Authorize]
        [TypeFilter( typeof( ValidateIPClaimActionFilter ) )]
        [Produces( typeof( ApiResponse<CreateReactionResult> ) )]
        public async Task<IActionResult> PostDislike([FromRoute]int shoutId)
        {
            return await this.PostReaction( shoutId, true, false );
        }

        [HttpPost( "{shoutId}/comments" )]
        [Authorize]
        [TypeFilter( typeof( ValidateIPClaimActionFilter ) )]
        [Produces( typeof( ApiResponse<CreateCommentResult> ) )]
        public async Task<IActionResult> PostComment([FromRoute]int shoutId, [FromBody]NewCommentDTO newCommentDTO)
        {
            ApiResponse<CreateCommentResult> apiResponse = new ApiResponse<CreateCommentResult>();

            try
            {
                apiResponse.EndpointResult = await this._shoutService.CreateCommentAsync(
                    newCommentDTO.ToComment().AddIds( shoutId, Convert.ToInt32( this._JWTService.GetClaimId( HttpContext.User ) ) )
                );

                if (apiResponse.EndpointResult.State != CreateShoutState.Success)
                {
                    apiResponse.Error = true;
                    apiResponse.Message = apiResponse.EndpointResult.State.Switch( new Dictionary<CreateShoutState, Func<string>>()
                    {
                        { CreateShoutState.ContentTooLong, () => Resource_ResponseMessages_en.ContentTooLong },
                        { CreateShoutState.ContentTooSmall, () => Resource_ResponseMessages_en.ContentTooSmall }
                    },
                        () => Resource_ResponseMessages_en.Unknown
                    );

                    return BadRequest( apiResponse );
                }

                apiResponse.Message = Resource_ResponseMessages_en.CreateCommentSuccess;

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
                return Problem( statusCode: 500, detail: Newtonsoft.Json.JsonConvert.SerializeObject( e, Newtonsoft.Json.Formatting.Indented ) );
            }
        }

        [HttpPost( "{shoutId}/comments/{commentId}/likes" )]
        [Authorize]
        [TypeFilter( typeof( ValidateIPClaimActionFilter ) )]
        [Produces( typeof( ApiResponse<CreateReactionResult> ) )]
        public async Task<IActionResult> PostCommentLike([FromRoute]int commentId)
        {
            return await this.PostReaction( commentId, false, true );
        }

        [HttpPost( "{shoutId}/comments/{commentId}/dislikes" )]
        [Authorize]
        [TypeFilter( typeof( ValidateIPClaimActionFilter ) )]
        [Produces( typeof( ApiResponse<CreateReactionResult> ) )]
        public async Task<IActionResult> PostCommentDislike([FromRoute]int commentId)
        {
            return await this.PostReaction( commentId, false, false );
        }

        private async Task<IActionResult> PostReaction(int entityId, bool isShout, bool isLike)
        {
            ApiResponse<CreateReactionResult> apiResponse = new ApiResponse<CreateReactionResult>();

            try
            {
                apiResponse.EndpointResult = await this._shoutService.CreateReactionAsync(
                    isShout ? EntityType.Shout : EntityType.Comment,
                    entityId,
                    Convert.ToInt32( _JWTService.GetClaimId( HttpContext.User ) ),
                    isLike ? ReactionTypeId.Like : ReactionTypeId.Dislike
                );

                if (apiResponse.EndpointResult.State != CreateReactionState.Success)
                {
                    apiResponse.Error = true;
                    apiResponse.Message = apiResponse.EndpointResult.State.Switch( new Dictionary<CreateReactionState, Func<string>>()
                    {
                        { CreateReactionState.ReactionExists, () => Resource_ResponseMessages_en.ReactionExists },
                        { CreateReactionState.BadRequest, () => Resource_ResponseMessages_en.BadRequest }
                    },
                        () => Resource_ResponseMessages_en.Unknown
                    );

                    return BadRequest( apiResponse );
                }

                apiResponse.Message = Resource_ResponseMessages_en.Success;

                return Ok( apiResponse );
            }
            catch (ArgumentOutOfRangeException e)
            {
                apiResponse.Error = true;
                apiResponse.Message = Resource_ResponseMessages_en.BadRequest;
                return BadRequest( apiResponse );
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
                return Problem( statusCode: 500, detail: Newtonsoft.Json.JsonConvert.SerializeObject( e, Newtonsoft.Json.Formatting.Indented ) );
            }
        }

        #endregion POST

        #region DELETE

        [HttpDelete("{shoutId}")]
        [Authorize]
        [TypeFilter( typeof( ValidateIPClaimActionFilter ) )]
        [Produces( typeof( ApiResponse<bool> ) )]
        public async Task<IActionResult> Delete([FromRoute]int shoutId)
        {
            ApiResponse<bool> apiResponse = new ApiResponse<bool>();

            try
            {
                await this._shoutService.DeleteAsync( shoutId, Convert.ToInt32( this._JWTService.GetClaimId( HttpContext.User ) ) );

                apiResponse.Message = Resource_ResponseMessages_en.DeleteShoutSuccess;
                apiResponse.EndpointResult = true;
                return Ok( apiResponse );
            }
            catch (ArgumentOutOfRangeException e)
            {
                apiResponse.Error = true;
                apiResponse.Message = Resource_ResponseMessages_en.BadRequest;
                return BadRequest( apiResponse );
            }
            catch (DbException e)
            {
                // TODO: Error handling.
                return Problem( statusCode: 500, detail: Newtonsoft.Json.JsonConvert.SerializeObject( e, Newtonsoft.Json.Formatting.Indented ) );
            }
            catch (Exception e)
            {
                return Problem( statusCode: 500, detail: Newtonsoft.Json.JsonConvert.SerializeObject( e, Newtonsoft.Json.Formatting.Indented ) );
            }
        }

        [HttpDelete( "{shoutId}/likes" )]
        [Authorize]
        [TypeFilter( typeof( ValidateIPClaimActionFilter ) )]
        [Produces( typeof( ApiResponse<bool> ) )]
        public async Task<IActionResult> DeleteShoutLike([FromRoute]int shoutId)
        {
            return await this.DeleteReaction( shoutId, true, true );
        }

        [HttpDelete( "{shoutId}/dislikes" )]
        [Authorize]
        [TypeFilter( typeof( ValidateIPClaimActionFilter ) )]
        [Produces( typeof( ApiResponse<bool> ) )]
        public async Task<IActionResult> DeleteShoutDislike([FromRoute]int shoutId)
        {
            return await this.DeleteReaction( shoutId, true, false );
        }

        [HttpDelete( "{shoutId}/comments/{commentId}" )]
        [Authorize]
        [TypeFilter( typeof( ValidateIPClaimActionFilter ) )]
        [Produces( typeof( ApiResponse<bool> ) )]
        public async Task<IActionResult> DeleteComment([FromRoute]int commentId)
        {
            ApiResponse<bool> apiResponse = new ApiResponse<bool>();

            try
            {
                await this._shoutService.DeleteCommentAsync( commentId, Convert.ToInt32( this._JWTService.GetClaimId( HttpContext.User ) ) );

                apiResponse.Message = Resource_ResponseMessages_en.DeleteCommentSuccess;
                apiResponse.EndpointResult = true;
                return Ok( apiResponse );
            }
            catch (ArgumentOutOfRangeException e)
            {
                apiResponse.Error = true;
                apiResponse.Message = Resource_ResponseMessages_en.BadRequest;
                return BadRequest( apiResponse );
            }
            catch (DbException e)
            {
                apiResponse.Error = true;
                apiResponse.Message = Resource_ResponseMessages_en.Unknown;
                return Problem( statusCode: 500, detail: Newtonsoft.Json.JsonConvert.SerializeObject( e, Newtonsoft.Json.Formatting.Indented ) );
            }
            catch (Exception e)
            {
                apiResponse.Error = true;
                apiResponse.Message = Resource_ResponseMessages_en.Unknown;
                return Problem( statusCode: 500, detail: Newtonsoft.Json.JsonConvert.SerializeObject( e, Newtonsoft.Json.Formatting.Indented ) );
            }
        }

        [HttpDelete( "{shoutId}/comments/{commentId}/likes" )]
        [Authorize]
        [TypeFilter( typeof( ValidateIPClaimActionFilter ) )]
        [Produces( typeof( ApiResponse<bool> ) )]
        public async Task<IActionResult> DeleteCommentLike([FromRoute]int commentId)
        {
            return await this.DeleteReaction( commentId, false, true );
        }

        [HttpDelete( "{shoutId}/comments/{commentId}/dislikes" )]
        [Authorize]
        [TypeFilter( typeof( ValidateIPClaimActionFilter ) )]
        [Produces( typeof( ApiResponse<bool> ) )]
        public async Task<IActionResult> DeleteCommentDislike([FromRoute]int commentId)
        {
            return await this.DeleteReaction( commentId, false, false );
        }

        private async Task<IActionResult> DeleteReaction(int entityId, bool isShout, bool isLike)
        {
            ApiResponse<bool> apiResponse = new ApiResponse<bool>();

            try
            {
                await this._shoutService.DeleteReactionAsync(
                    isShout ? EntityType.Shout : EntityType.Comment,
                    new ShoutReaction()
                    {
                        ReactionTypeId = isLike ? ReactionTypeId.Like : ReactionTypeId.Dislike,
                        UserId = Convert.ToInt32( this._JWTService.GetClaimId( HttpContext.User ) )
                    },
                    entityId
                );

                apiResponse.Message = Resource_ResponseMessages_en.Success;
                apiResponse.EndpointResult = true;
                return Ok( apiResponse );
            }
            catch (ArgumentOutOfRangeException e)
            {
                apiResponse.Error = true;
                apiResponse.Message = Resource_ResponseMessages_en.BadRequest;
                return BadRequest( apiResponse );
            }
            catch (DbException e)
            {
                apiResponse.Error = true;
                apiResponse.Message = Resource_ResponseMessages_en.Unknown;
                return Problem( statusCode: 500, detail: Newtonsoft.Json.JsonConvert.SerializeObject( e, Newtonsoft.Json.Formatting.Indented ) );
            }
            catch (Exception e)
            {
                apiResponse.Error = true;
                apiResponse.Message = Resource_ResponseMessages_en.Unknown;
                return Problem( statusCode: 500, detail: Newtonsoft.Json.JsonConvert.SerializeObject( e, Newtonsoft.Json.Formatting.Indented ) );
            }
        }

        #endregion DELETE

        #endregion PUBLIC ENDPOINTS
    }
}
