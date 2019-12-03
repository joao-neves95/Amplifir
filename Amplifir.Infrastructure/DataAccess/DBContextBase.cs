﻿using System;
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

        protected DbConnection _dbConnection;

        public DbConnection DbConnection { get; private set; }

        protected readonly string _connectionString = null;

        #region IDisposable Support

        private bool disposedValue = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                this._dbConnection.Dispose();
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
            Dispose( true );
            GC.SuppressFinalize( this );
        }

        #endregion IDisposable Support
    }
}
