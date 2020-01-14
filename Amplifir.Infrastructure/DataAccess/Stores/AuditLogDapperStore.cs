/*
 * Copyright (c) 2019 - 2020 João Pedro Martins Neves (SHIVAYL) - All Rights Reserved.
 *
 * Amplifir and all its content is licensed under the GNU Lesser General Public License (LGPL),
 * version 3, located in the root of this project, under the name "LICENSE.md".
 *
 */

using Dapper;
using System.Threading.Tasks;
using Amplifir.Core.Interfaces;
using Amplifir.Core.Entities;

namespace Amplifir.Infrastructure.DataAccess.Stores
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage( "Reliability", "CA2007:Consider calling ConfigureAwait on the awaited task", Justification = "<Pending>" )]
    public class AuditLogDapperStore : DBStoreBase, IAuditLogStore
    {
        public AuditLogDapperStore(IDBContext dBContext) : base( dBContext )
        {
        }

        public async Task<int> CreateLogAsync(AuditLog auditLog)
        {
            await base._dBContext.OpenDBConnectionAsync();

            using (base._dBContext.DbConnection)
            {
                return await base._dBContext.DbConnection.ExecuteAsync(
                    DapperHelperQueries.CreateNewLog( auditLog.UserId.ToString(), auditLog.EventTypeId ),
                    new { IPv4 = auditLog.IPv4 }
                );
            }
        }
    }
}
