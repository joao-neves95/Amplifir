/*
 * Copyright (c) 2019 - 2020 João Pedro Martins Neves (SHIVAYL) - All Rights Reserved.
 *
 * Amplifir and all its content is licensed under the GNU Lesser General Public License (LGPL),
 * version 3, located in the root of this project, under the name "LICENSE.md".
 *
 */

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
