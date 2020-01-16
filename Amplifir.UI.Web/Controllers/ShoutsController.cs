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

// TODO: Before production, remove every API exception returns.

namespace Amplifir.UI.Web.Controllers
{
    [Route("api/shouts")]
    [ApiController]
    [System.Diagnostics.CodeAnalysis.SuppressMessage( "Reliability", "CA2007:Consider calling ConfigureAwait on the awaited task", Justification = "<Pending>" )]
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
        [Produces( typeof( ApiResponse<CreateShoutResult> ) )]
        public async Task<IActionResult> Post([FromBody]NewShoutDTO newShoutDTO)
        {
            ApiResponse<CreateShoutResult> apiResponse = new ApiResponse<CreateShoutResult>();

            try
            {
                newShoutDTO.UserId = Convert.ToInt32( this._JWTService.GetClaimId( HttpContext.User ) );
                apiResponse.EndpointResult = await this._shoutService.CreateAsync( newShoutDTO as Shout );

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

                return Ok( apiResponse );
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

        [HttpPost("{shoutId}/likes")]
        [Authorize]
        [Produces( typeof( ApiResponse<CreateReactionResult> ) )]
        public async Task<IActionResult> PostLike([FromRoute]int shoutId)
        {
            return await this.PostReaction( shoutId, true, true );
        }

        [Authorize]
        [HttpPost( "{shoutId}/dislikes" )]
        [Produces( typeof( ApiResponse<CreateReactionResult> ) )]
        public async Task<IActionResult> PostDislike([FromRoute]int shoutId)
        {
            return await this.PostReaction( shoutId, true, false );
        }

        [HttpPost( "{shoutId}/comments" )]
        [Authorize]
        [Produces( typeof( ApiResponse<CreateCommentResult> ) )]
        public async Task<IActionResult> PostComment([FromRoute]int shoutId, [FromBody]NewCommentDTO newCommentDTO)
        {
            ApiResponse<CreateCommentResult> apiResponse = new ApiResponse<CreateCommentResult>();

            try
            {
                newCommentDTO.UserId = Convert.ToInt32( this._JWTService.GetClaimId( HttpContext.User ) );
                apiResponse.EndpointResult = await this._shoutService.CreateCommentAsync( newCommentDTO as Comment );

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

                return Ok( apiResponse );
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

        [HttpPost( "{shoutId}/comments/{commentId}/likes" )]
        [Authorize]
        [Produces( typeof( ApiResponse<CreateReactionResult> ) )]
        public async Task<IActionResult> PostCommentLike([FromRoute]int commentId)
        {
            return await this.PostReaction( commentId, false, true );
        }

        [HttpPost( "{shoutId}/comments/{commentId}/dislikes" )]
        [Authorize]
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

                return Ok( apiResponse );
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

        #endregion POST

        #region DELETE

        [HttpDelete("{shoutId}")]
        [Authorize]
        [Produces( typeof( ApiResponse<bool> ) )]
        public async Task<IActionResult> Delete([FromRoute]int shoutId)
        {
            ApiResponse<bool> apiResponse = new ApiResponse<bool>();

            try
            {
                await this._shoutService.DeleteAsync( shoutId, Convert.ToInt32( this._JWTService.GetClaimId( HttpContext.User ) ) );

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

        #endregion DELETE

        #endregion PUBLIC ENDPOINTS
    }
}
