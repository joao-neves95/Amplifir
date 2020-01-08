using System;
using System.Data;
using System.Data.Common;

namespace Amplifir.Infrastructure.DataAccess.Interfaces
{
    public abstract class DBContextBase : IDisposable
    {
        public DBContextBase()
        {
        }

        public DBContextBase(string connectionString)
        {
            this._connectionString = connectionString;
        }

        internal protected readonly string _connectionString;

        internal protected DbConnection _dbConnection;

        public DbConnection DbConnection
        {
            get
            {
                return this._dbConnection;
            }
        }

        #region IDisposable Support

        private bool disposedValue = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue && this._dbConnection != null)
            {
                if (this._dbConnection.State == ConnectionState.Open)
                {
                    this._dbConnection.CloseAsync();
                }

                if (this._dbConnection.State != ConnectionState.Connecting &&
                    this._dbConnection.State != ConnectionState.Executing &&
                    this._dbConnection.State != ConnectionState.Fetching
                )
                {
                    this._dbConnection.DisposeAsync();
                }

                this._dbConnection = null;
                disposedValue = true;
            }
        }

        ~DBContextBase()
        {
            Dispose( false );
        }

        public void Dispose()
        {
            Dispose( false );
            GC.SuppressFinalize( this );
        }

        #endregion IDisposable Support
    }
}
