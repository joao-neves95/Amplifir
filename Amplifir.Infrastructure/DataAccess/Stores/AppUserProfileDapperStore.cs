using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Amplifir.Core.Entities;
using Amplifir.Core.Interfaces;

namespace Amplifir.Infrastructure.DataAccess.Stores
{
    public class AppUserProfileDapperStore : DBStoreBase, IAppUserProfileStore
    {
        public AppUserProfileDapperStore(IDBContext dBContext) : base( dBContext )
        {
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage( "Reliability", "CA2007:Consider calling ConfigureAwait on the awaited task", Justification = "<Pending>" )]
        public async Task<AppUserProfile> GetByUserIdAsync(int userId)
        {
            await base._dBContext.OpenDBConnectionAsync();

            using (base._dBContext.DbConnection)
            {
                return await base._dBContext.DbConnection.QueryFirstOrDefaultAsync<AppUserProfile>(
                    $@"SELECT Id, UserId, Bio, FollowingCount, FollowersCount, UserLocation, BirthDate
                       FROM AppUserProfile
                       WHERE UserId = {userId}
                    "
                );
            }
        }
    }
}
