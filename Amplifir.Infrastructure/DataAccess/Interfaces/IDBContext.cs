using System;
using System.Threading.Tasks;
using System.Data.Common;

namespace Amplifir.Infrastructure.DataAccess.Interfaces
{
    interface IDBContext : IDisposable
    {
        Task<DbConnection> OpenDBConnectionAsync(string connectionString);
    }
}
