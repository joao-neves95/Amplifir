/*
 * Copyright (c) 2019 - 2020 João Pedro Martins Neves (SHIVAYL) - All Rights Reserved.
 *
 * Amplifir and all its content is licensed under the GNU Lesser General Public License (LGPL),
 * version 3, located in the root of this project, under the name "LICENSE.md".
 *
 */

using System;
using System.Text;
using System.Collections.Generic;
using System.Threading.Tasks;
using Dapper;
using Amplifir.Core.Constants;
using Amplifir.Core.Enums;
using Amplifir.Core.Interfaces;
using Amplifir.Core.Entities;

namespace Amplifir.Infrastructure.DataAccess.Stores
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage( "Reliability", "CA2007:Consider calling ConfigureAwait on the awaited task", Justification = "<Pending>" )]
    public class ShoutDapperStore : DBStoreBase, IShoutStore
    {
        public ShoutDapperStore(IDBContext dBContext) : base( dBContext )
        {
        }

        #region CREATE

        public async Task<int> CreateAsync(Shout newShout)
        {
            await base._dBContext.OpenDBConnectionAsync();

            using (base._dBContext.DbConnection)
            {
                return await base._dBContext.DbConnection.ExecuteScalarAsync<int>(
                    $@"INSERT INTO Shout (UserId, Content)
                       VALUES (@UserId, @Content);
                       { DapperHelperQueries.SelectSessionLastInsertedShoutId() }
                    ",
                    new { UserId = newShout.UserId, Content = newShout.Content }
                );
            }

        }

        public async Task<int> CreateHashtagAsync(List<string> hashtags)
        {
            List<object> parameters = new List<object>();

            for (int i = 0; i < hashtags.Count; ++i)
            {
                parameters.Add( new { Content = hashtags[i] } );
            }

            return await this.CreateHashtagAsync( parameters );
        }

        public async Task<int> CreateHashtagAsync(string hashtag)
        {
            return await this.CreateHashtagAsync( new { Content = hashtag } );
        }

        private async Task<int> CreateHashtagAsync(object parameters = null)
        {
            await base._dBContext.OpenDBConnectionAsync();

            using (base._dBContext.DbConnection)
            {
                return await base._dBContext.DbConnection.ExecuteAsync(
                    @"INSERT INTO Hashtag (Content)
                      VALUES (@Content)",
                    parameters
                );
            }
        }

        public async Task<int> AddShoutToExistingHashtag(int shoutId, string hashtag)
        {
            return await this.AddShoutToExistingHashtag(
                new { ShoutId = shoutId, Hashtag = hashtag },
                new { Content = hashtag }
            );
        }

        public async Task<int> AddShoutToExistingHashtag(int shoutId, List<string> hashtags)
        {
            if (hashtags.Count == 0)
            {
                return 0;
            }

            List<DynamicParameters> hashtagShoutParameters = new List<DynamicParameters>();
            List<DynamicParameters> hashtagParameters = new List<DynamicParameters>();
            string currentHashtag;
            int i;

            for (i = 0; i < hashtags.Count; ++i)
            {
                currentHashtag = hashtags[i];

                hashtagShoutParameters.Add( new DynamicParameters( new { ShoutId = shoutId, Hashtag = currentHashtag } ) );
                hashtagParameters.Add( new DynamicParameters( new { Content = currentHashtag } ) );
            }

            return await this.AddShoutToExistingHashtag( hashtagShoutParameters, hashtagParameters );
        }

        private async Task<int> AddShoutToExistingHashtag(object hashtagShoutParameters, object hashtagParameters)
        {
            return await base._dBContext.ExecuteTransactionAsync( new Dictionary<string, object>()
            {
                {
                    @"INSERT INTO HashtagShout (ShoutId, HashtagId)
                      VALUES (
                          @ShoutId,
                          (
                              SELECT Id
                              FROM Hashtag
                              WHERE Content = @Hashtag 
                          ) 
                      )
                    ",
                    hashtagShoutParameters
                },
                {
                    $@"UPDATE Hashtag
                       SET ShoutCount = ShoutCount + 1
                       WHERE Content = @Content
                    ",
                    hashtagParameters
                }
            } );
        }

        public async Task<int> CreateReactionAsync(EntityType entityType, int entityId, int userId, short reactionTypeId)
        {
            string reactionTableName = entityType == EntityType.Shout ? TableNames.ShoutReaction : TableNames.CommentReaction;
            string entityTableName = entityType == EntityType.Shout ? TableNames.Shout : TableNames.Comment;

            string reactionType = reactionTypeId == ReactionTypeId.Like ?
                ReactionTypeColumnNames.Like :
                ReactionTypeColumnNames.Dislike;

            await base._dBContext.ExecuteTransactionAsync( new Dictionary<string, object>()
            {
                {
                    $@"INSERT INTO {reactionTableName} ({entityTableName}Id, UserId, ReactionTypeId)
                       VALUES ( {entityId.ToString()}, {userId.ToString()}, {reactionTypeId.ToString()} )",
                    null
                },
                {
                    $@"UPDATE {entityTableName}
                       SET {reactionType} = {reactionType} + 1
                       WHERE Id = {entityId}",
                    null
                }

            }, false );

            // Reuse "shoutId" variable to alocate less memory.
            entityId = await base._dBContext.DbConnection.ExecuteScalarAsync<int>( DapperHelperQueries.SelectSessionLastInserted( reactionTableName, "id" ) );
            // Do not await.
            _ = base._dBContext.DbConnection.DisposeAsync();
            return entityId;
        }

        public async Task<int> CreateCommentAsync(Comment newComment)
        {
            return await base._dBContext.DbConnection.ExecuteScalarAsync<int>(
                $@"INSERT INTO Comment (ShoutId, UserId, Content)
                   VALUES ({ newComment.ShoutId.ToString() }, { newComment.UserId.ToString() }, @Content);
                   ${DapperHelperQueries.SelectSessionLastInserted( "Comment", "id" )}
                ",
                new { Content = newComment.Content }
            );
        }

        #endregion CREATE

        #region GET

        public async Task<Shout> GetByIdAsync(int shoutId)
        {
            await base._dBContext.OpenDBConnectionAsync();

            using (base._dBContext.DbConnection)
            {
                return await base._dBContext.DbConnection.QueryFirstOrDefaultAsync<Shout>(
                    $@"{DapperHelperQueries.GetShoutQueryWithoutWhere()}
                       WHERE Id = { shoutId }
                    "
                );
            }
        }

        public async Task<List<Shout>> GetByUserIdAsync(int userId, int lastId = 0, short limit = 10)
        {
            await base._dBContext.OpenDBConnectionAsync();

            using (base._dBContext.DbConnection)
            {
                return (List<Shout>)await base._dBContext.DbConnection.QueryAsync<Shout>(
                    $@"{DapperHelperQueries.GetShoutQueryWithoutWhere()}
                       WHERE Shout.UserId = { userId } AND {DapperHelperQueries.PaginatedQueryDESC( "Shout", lastId, limit )}
                    "
                );
            }
        }

        public async Task<List<Shout>> GetFollowingShoutsByUserIdAsync(int userId, int lastId = 0, short limit = 10)
        {
            throw new NotImplementedException();

            await base._dBContext.OpenDBConnectionAsync();

            using (base._dBContext.DbConnection)
            {
                return (List<Shout>)await base._dBContext.DbConnection.QueryAsync<Shout>(
                    $@"{DapperHelperQueries.GetShoutQueryWithoutWhere()}
                       WHERE {DapperHelperQueries.PaginatedQueryDESC( "Shout", lastId, limit )}
                    "
                );
            }
        }

        public async Task<List<string>> GetHashtagsAsync( List<string> hashtags )
        {
            if (hashtags.Count == 0)
            {
                return new List<string>();
            }

            StringBuilder parameterNames = new StringBuilder();
            DynamicParameters parameters = new DynamicParameters();

            for (int i = 0; i < hashtags.Count; ++i)
            {
                parameters.Add( "Value" + i.ToString(), hashtags[i] );
                parameterNames.Append( "@Value" ).Append( i.ToString() );

                if (i < hashtags.Count - 1)
                {
                    parameterNames.Append( ", " );
                }
            }

            await base._dBContext.OpenDBConnectionAsync();

            using (base._dBContext.DbConnection)
            {
                return (List<string>)await base._dBContext.DbConnection.QueryAsync<string>(
                    $@"SELECT Content
                       FROM Hashtag
                       WHERE Content IN( { parameterNames.ToString() } )",
                    parameters
                );
            }
        }

        public Task<List<Comment>> GetCommentsByShoutIdAsync(int shoutId, int lastId = 0, int limit = 10)
        {
            throw new NotImplementedException();
        }

        public async Task<ShoutReaction> GetShoutReactionAsync(int shoutId, int userId)
        {
            await base._dBContext.OpenDBConnectionAsync();

            using (base._dBContext.DbConnection)
            {
                return await base._dBContext.DbConnection.QueryFirstOrDefaultAsync<ShoutReaction>(
                    this.GetReactionQuery( EntityType.Shout, shoutId, userId )
                );
            }
        }

        public async Task<CommentReaction> GetCommentReactionAsync(int commentId, int userId)
        {
            await base._dBContext.OpenDBConnectionAsync();

            using (base._dBContext.DbConnection)
            {
                return await base._dBContext.DbConnection.QueryFirstOrDefaultAsync<CommentReaction>(
                    this.GetReactionQuery( EntityType.Comment, commentId, userId )
                );
            }
        }

        private string GetReactionQuery(EntityType entityType, int entityId, int userId)
        {
            return $@"SELECT *
                      FROM {(entityType == EntityType.Shout ? TableNames.ShoutReaction : TableNames.CommentReaction) }
                      WHERE UserId = {userId.ToString()} AND {(entityType == EntityType.Shout ? TableNames.Shout : TableNames.Comment)}Id = {entityId.ToString()}
                   ";
        }

        #region EXISTS

        public async Task<bool> HashtagExists(string hashtag)
        {
            await base._dBContext.OpenDBConnectionAsync();

            using (base._dBContext.DbConnection)
            {
                return await base._dBContext.DbConnection.ExecuteScalarAsync<int>(
                    DapperHelperQueries.Exists( "Hashtag", "Content" ),
                    new { Value1 = hashtag }

                ) == 1;
            }
        }

        public async Task<bool> UserReactionExistsAsync(int shoutId, int userId)
        {
            await base._dBContext.OpenDBConnectionAsync();

            using (base._dBContext.DbConnection)
            {
                return await base._dBContext.DbConnection.ExecuteScalarAsync<int>(
                    DapperHelperQueries.Exists( "ShoutReaction", new string[] { "ShoutId", "UserId" } ),
                    new { Value1 = shoutId, Value2 = userId }

                ) == 1;
            }
        }

        #endregion EXISTS

        #endregion GET

        #region DELETE

        public async Task<int> DeleteAsync(int shoutId, int userId)
        {
            return await base._dBContext.ExecuteTransactionAsync( new Dictionary<string, object>()
            {
                {
                    $@"
                    DELETE FROM ShoutAsset
                    WHERE ShoutId = { shoutId };
                    
                    DELETE FROM CommentReaction
                    WHERE CommentId IN (
                        SELECT Id
                        FROM Comment
                        WHERE ShoutId = { shoutId }
                    );
                    
                    DELETE FROM Comment
                    WHERE ShoutId = { shoutId };
                    
                    DELETE FROM ShoutReaction
                    WHERE ShoutId = { shoutId };
                    
                    DELETE FROM HashtagShout
                    WHERE ShoutId = { shoutId };
                    
                    DELETE FROM Shout
                                               -- Enforce that the Shout is from the provided user id.
                    WHERE Id = { shoutId } AND UserId = { userId };
                    ",
                    null
                }
            } );
        }

        public async Task<int> DeleteCommentByIdAsync(int id, int userId)
        {
            return await base._dBContext.ExecuteTransactionAsync( new Dictionary<string, object>()
            {
                {
                    $@"                  
                    DELETE FROM CommentReaction
                    WHERE ShoutId = { id }
                    
                    DELETE FROM Comment
                    WHERE Id = { id } AND UserId = { userId };
                    ",
                    null
                }
            } );
        }

        public async Task<int> DeleteReactionByIdAsync(EntityType entityType, ReactionBase reaction, int entityId)
        {
            string reactionName = reaction.ReactionTypeId == ReactionTypeId.Like ? ReactionTypeColumnNames.Like : ReactionTypeColumnNames.Dislike;

            await base._dBContext.OpenDBConnectionAsync();

            using (base._dBContext.DbConnection)
            {
                return await base._dBContext.ExecuteTransactionAsync( new Dictionary<string, object>()
                {
                    {
                        $@"DELETE FROM {( entityType == EntityType.Shout ? TableNames.ShoutReaction : TableNames.CommentReaction )}
                           WHERE Id = { reaction.Id } AND UserId = { reaction.UserId }",
                        null
                    },
                    {
                        $@"UPDATE {( entityType == EntityType.Shout ? TableNames.Shout : TableNames.Comment )}
                           SET {reactionName} = {reactionName} + 1
                           WHERE Id = {entityId}",
                        null
                    }
                } );
            }
        }

        #endregion DELETE
    }
}
