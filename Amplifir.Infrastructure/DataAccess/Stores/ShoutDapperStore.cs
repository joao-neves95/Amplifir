using System;
using System.Collections.Generic;
using System.Text;
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
    }
}
