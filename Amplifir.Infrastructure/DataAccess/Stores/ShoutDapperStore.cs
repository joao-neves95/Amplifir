using System;
using System.Text;
using System.Collections.Generic;
using System.Threading.Tasks;
using Dapper;
using Amplifir.Core.Constants;
using Amplifir.Core.Interfaces;
using Amplifir.Core.Entities;
using Amplifir.Core.Enums;

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
            return await base._dBContext.DbConnection.ExecuteScalarAsync<int>(
                $@"INSERT INTO Shout (UserId, Content)
                   VALUES (@UserId, @Content);
                   ${DapperHelperQueries.SelectSessionLastInsertedShoutId()}
                ",
                new { @UserId = newShout.UserId, @Content = newShout.Content }
            );
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
                shoutId,
                new { ShoutId = shoutId, Hashtag = hashtag },
                new { Content = hashtag }
            );
        }

        public async Task<int> AddShoutToExistingHashtag(int shoutId, List<string> hashtags)
        {
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

            return await this.AddShoutToExistingHashtag( shoutId, hashtagShoutParameters, hashtagParameters );
        }

        private async Task<int> AddShoutToExistingHashtag(int shoutId, object hashtagShoutParameters, object hashtagParameters)
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
            entityId = await base._dBContext.DbConnection.ExecuteScalarAsync<int>( DapperHelperQueries.Exists( reactionTableName, "id" ) );
            // Do not await.
            _ = base._dBContext.DbConnection.DisposeAsync();
            return entityId;
        }

        public async Task<int> CreateCommentAsync(Comment newComment)
        {
            return await base._dBContext.DbConnection.ExecuteScalarAsync<int>(
                $@"INSERT INTO Comment (UserId, Content)
                   VALUES (@UserId, @Content);
                   ${DapperHelperQueries.SelectSessionLastInsertedShoutId()}
                ",
                new { @UserId = newComment.UserId, @Content = newComment.Content }
            );
        }

        #endregion CREATE

        #region GET

        public async Task<Shout> GetByIdAsync(int shoutId)
        {
            throw new NotImplementedException();
        }

        public async Task<Shout> GetByUserIdAsync(int userId, int lastId = 0, short limit = 10)
        {
            throw new NotImplementedException();
        }

        public async Task<Shout> GetFollowingShoutsByUserIdAsync(int userId, int lastId = 0, short limit = 10)
        {
            throw new NotImplementedException();
        }

        public async Task<List<string>> GetHashtagsAsync( List<string> hashtag )
        {
            StringBuilder parameterNames = new StringBuilder();
            DynamicParameters parameters = new DynamicParameters();

            for (int i = 0; i < hashtag.Count; ++i)
            {
                parameters.Add( "Value" + i.ToString(), hashtag[i] );
                parameterNames.Append( "@Value" ).Append( i.ToString() );

                if (i < hashtag.Count - 1)
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
                    WHERE ShoutId IN (
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
                    WHERE Id = { shoutId } AND UserId = { userId };
                    ",
                    // Enforce that the Shout is from the provided user id.
                    null
                }
            } );
        }

        public async Task<int> DeleteReactionAsync(EntityType entityType, int entityId, int userId)
        {
            await base._dBContext.OpenDBConnectionAsync();

            using (base._dBContext.DbConnection)
            {
                return await base._dBContext.DbConnection.ExecuteAsync(
                    $@"DELETE FROM {(entityType == EntityType.Shout ? TableNames.ShoutReaction : TableNames.CommentReaction)}
                       WHERE Id = {entityId} AND UserId = {userId}"
                );
            }

            throw new NotImplementedException();
        }

        #endregion DELETE
    }
}
