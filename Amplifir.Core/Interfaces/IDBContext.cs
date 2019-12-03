using System;
using System.Threading.Tasks;
using System.Data.Common;
using System.Collections.Generic;

namespace Amplifir.Core.Interfaces
{
    public interface IDBContext : IDisposable
    {
        DbConnection DbConnection { get; }

        /// <summary>
        /// 
        /// To use this overloaded method without a connection string, it is necessary
        /// to instantiate the class with the connection string.
        /// 
        /// </summary>
        Task<DbConnection> OpenDBConnectionAsync();

        Task<DbConnection> OpenDBConnectionAsync(string connectionString);

        /// <summary>
        /// 
        /// It opens the connection and executes a transaction based on a dictionary with the sql code as key and parameters as value.
        /// If there is no parameters to sanitize, set the value null.
        /// 
        /// </summary>
        /// <param name="sqlAndParameters"></param>
        Task<int> ExecuteTransactionAsync(Dictionary<string, object> sqlAndParameters);
    }
}
