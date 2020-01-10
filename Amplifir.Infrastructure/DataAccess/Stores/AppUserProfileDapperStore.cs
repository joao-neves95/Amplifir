using System.Threading.Tasks;
using Dapper;
using Amplifir.Core.Interfaces;
using Amplifir.Core.Entities;

namespace Amplifir.Infrastructure.DataAccess.Stores
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage( "Reliability", "CA2007:Consider calling ConfigureAwait on the awaited task", Justification = "<Pending>" )]
    public class AppUserProfileDapperStore : DBStoreBase, IAppUserProfileStore
    {
        public AppUserProfileDapperStore(IDBContext dBContext) : base( dBContext )
        {
        }

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
