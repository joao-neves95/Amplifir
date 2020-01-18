/*
 * Copyright (c) 2019 - 2020 João Pedro Martins Neves (SHIVAYL) - All Rights Reserved.
 *
 * Amplifir and all its content is licensed under the GNU Lesser General Public License (LGPL),
 * version 3, located in the root of this project, under the name "LICENSE.md".
 *
 */

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
                    $@"SELECT Id, UserId, Bio, Website, UserLocation, BirthDate, FollowingCount, FollowersCount
                       FROM AppUserProfile
                       WHERE UserId = {userId}
                    "
                );
            }
        }
    }
}
