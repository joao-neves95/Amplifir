﻿using Dapper;
using System.Threading.Tasks;
using Amplifir.Core.Entities;
using Amplifir.Core.Interfaces;

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
