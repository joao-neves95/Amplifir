using System;
using System.Collections.Generic;
using System.Text;
using Amplifir.Core.Interfaces;

namespace Amplifir.Infrastructure.DataAccess.Stores
{
    public class ShoutStore : DBStoreBase
    {
        public ShoutStore(IDBContext dBContext) : base( dBContext )
        {
        }
    }
}
