using System.Threading.Tasks;
using System;
using System.Data.Common;
using Npgsql;
using Amplifir.Infrastructure.DataAccess.Interfaces;

namespace Amplifir.Infrastructure.DataAccess
{
    public class DapperDBContext : IDBContext
    {
        #region IDisposable Support

        private bool disposedValue = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                this._dbConnection.Dispose();
                disposedValue = true;
            }
        }

        ~DapperDBContext()
        {
            Dispose( false );
        }

        public void Dispose()
        {
            Dispose( true );
            GC.SuppressFinalize(this);
        }

        #endregion IDisposable Support

        protected DbConnection _dbConnection;

        public async Task<DbConnection> OpenDBConnectionAsync(string connectionString)
        {
            this._dbConnection = new NpgsqlConnection( connectionString );
            await this._dbConnection.OpenAsync();
            return this._dbConnection;
        }
    }
}
