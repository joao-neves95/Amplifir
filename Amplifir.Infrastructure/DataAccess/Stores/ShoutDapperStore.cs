using System;
using System.Collections.Generic;
using System.Text;
using Amplifir.Core.Interfaces;

namespace Amplifir.Infrastructure.DataAccess.Stores
{
    public class ShoutDapperStore : DBStoreBase
    {
        public ShoutDapperStore(IDBContext dBContext) : base( dBContext )
        {
        }
    }
}
