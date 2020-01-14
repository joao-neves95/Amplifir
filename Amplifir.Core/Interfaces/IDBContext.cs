/*
 * Copyright (c) 2019 - 2020 João Pedro Martins Neves (SHIVAYL) - All Rights Reserved.
 *
 * Amplifir and all its content is licensed under the GNU Lesser General Public License (LGPL),
 * version 3, located in the root of this project, under the name "LICENSE.md".
 *
 */

using System;
using System.Threading.Tasks;
using System.Data.Common;
using System.Collections.Generic;

namespace Amplifir.Core.Interfaces
{
    public interface IDBContext : IDisposable
    {
        DbConnection DbConnection { get; }

        Task<DbConnection> OpenDBConnectionAsync();

        Task<DbConnection> OpenDBConnectionAsync( string connectionString );

        /// <summary>
        /// 
        /// It opens the connection and executes a transaction based on a dictionary with the sql code as key and parameters as value.
        /// If there is no parameters to sanitize, set the value null.
        /// 
        /// </summary>
        /// <param name="sqlAndParameters"></param>
        Task<int> ExecuteTransactionAsync( Dictionary<string, object> sqlAndParameters, bool disposeConnection = true );
    }
}
