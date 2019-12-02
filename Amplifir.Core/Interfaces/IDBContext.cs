using System;
using System.Threading.Tasks;
using System.Data.Common;

namespace Amplifir.Core.Interfaces
{
    public interface IDBContext : IDisposable
    {
        /// <summary>
        /// 
        /// To use this overloaded method without a connection string, it is necessary
        /// to instantiate the class with the connection string.
        /// 
        /// </summary>
        Task<DbConnection> OpenDBConnectionAsync();

        Task<DbConnection> OpenDBConnectionAsync(string connectionString);
    }
}
