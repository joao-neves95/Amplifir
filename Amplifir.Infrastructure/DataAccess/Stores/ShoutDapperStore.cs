using System;
using System.Text;
using System.Collections.Generic;
using System.Threading.Tasks;
using Dapper;
using Amplifir.Core.Entities;
using Amplifir.Core.Interfaces;

namespace Amplifir.Infrastructure.DataAccess.Stores
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage( "Reliability", "CA2007:Consider calling ConfigureAwait on the awaited task", Justification = "<Pending>" )]
    public class ShoutDapperStore : DBStoreBase, IShoutStore
    {
        public ShoutDapperStore(IDBContext dBContext) : base( dBContext )
        {
        }

        public async Task<int> CreateAsync(Shout newShout)
        {
            throw new NotImplementedException();

            // TODO: Finish .CreateAsync()
            return await base._dBContext.ExecuteTransactionAsync( new Dictionary<string, object>()
            {
                {
                    @"INSERT INTO Shout (UserId, Content)
                        VALUES (@UserId, @Content)
                    ",
                    new { @UserId = newShout.UserId, @Content = newShout.Content }
                },
                {
                    @"INSERT INTO Hashtag
                      VALUES ",
                    null
                },
                {
                    @"INSERT INTO ShoutAsset ()
                        VALUES (@)
                    ",
                    null
                },
            } );
        }

        public Task<int> CreateHashtagAsync(string[] hashtag)
        {
            throw new NotImplementedException();
        }

        public Task<int> CreateHashtagAsync(List<string> hashtags)
        {
            throw new NotImplementedException();
        }

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

        public async Task<bool> HashtagExists(string hashtag)
        {
            await base._dBContext.OpenDBConnectionAsync();

            using (base._dBContext.DbConnection)
            {
                return await base._dBContext.DbConnection.ExecuteScalarAsync<int>(
                    DapperHelperQueries.Exists( "Hashtag", "Content" ),
                    new { Value = hashtag }

                ) == 1;
            }
        }

        public Task<int> IncrementHashtagShoutCountAsync(List<string> hashtags)
        {
            throw new NotImplementedException();
        }
    }
}
