using System;
using Amplifir.Core.Interfaces;

namespace Amplifir.Infrastructure.DataAccess
{
    public class DBStoreBase
    {
        protected readonly IDBContext _dBContext;

        public DBStoreBase(IDBContext dBContext)
        {
            this._dBContext = dBContext;
        }

        #region IDisposable Support

        private bool disposedValue = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                this._dBContext.Dispose();
                disposedValue = true;
            }
        }

        ~DBStoreBase()
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
