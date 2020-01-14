/*
 * Copyright (c) 2019 - 2020 João Pedro Martins Neves (SHIVAYL) - All Rights Reserved.
 *
 * Amplifir and all its content is licensed under the GNU Lesser General Public License (LGPL),
 * version 3, located in the root of this project, under the name "LICENSE.md".
 *
 */

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Data.Common;
using Npgsql;
using Dapper;
using Amplifir.Core.Utilities;
using Amplifir.Core.Interfaces;
using Amplifir.Infrastructure.DataAccess.Interfaces;

namespace Amplifir.Infrastructure.DataAccess
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage( "Reliability", "CA2007:Consider calling ConfigureAwait on the awaited task", Justification = "<Pending>" )]
    public class DapperDBContext : DBContextBase, IDBContext
    {
        public DapperDBContext()
        {
        }

        public DapperDBContext(string connectionString) : base (connectionString)
        {
        }

        public async Task<DbConnection> OpenDBConnectionAsync()
        {
            if (base._dbConnection?.State == System.Data.ConnectionState.Open)
            {
                return base._dbConnection;
            }

            return await this.OpenDBConnectionAsync( !String.IsNullOrEmpty(this._connectionString) ?
                this._connectionString :
                StringUtils.BuildPostreSQLConnectionStringWithSSL(
                    Environment.GetEnvironmentVariable( "DB_SERVER" ),
                    Environment.GetEnvironmentVariable( "DB_PORT" ),
                    Environment.GetEnvironmentVariable( "DB_DATABASE" ),
                    Environment.GetEnvironmentVariable( "DB_USER" ),
                    Environment.GetEnvironmentVariable( "DB_PASSWORD" )
                )
           );
        }

        public async Task<DbConnection> OpenDBConnectionAsync(string connectionString)
        {
            try
            {
                // TODO: Add Retry logic in case of exception.
                base._dbConnection = new NpgsqlConnection( connectionString );
                await base._dbConnection.OpenAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine( e.Message );
                Console.WriteLine( e.StackTrace );
                throw e;
            }

            return base._dbConnection;
        }

        #region WRAPPER METHODS

        public async Task<int> ExecuteTransactionAsync( Dictionary<string, object> sqlAndParameters, bool disposeConnection = true )
        {
            DbTransaction dbTransaction = null;

            try
            {
                await this.OpenDBConnectionAsync();

                using (dbTransaction = await base._dbConnection.BeginTransactionAsync())
                {
                    int affectedCollumnsNum = 0;

                    foreach (KeyValuePair<string, object> sqlAndParameter in sqlAndParameters)
                    {
                        await base._dbConnection.ExecuteAsync( sqlAndParameter.Key, sqlAndParameter.Value, dbTransaction );
                        ++affectedCollumnsNum;
                    }

                    await dbTransaction.CommitAsync();
                    return affectedCollumnsNum;
                }
            }
            catch (DbException e)
            {
                await dbTransaction?.RollbackAsync();
                disposeConnection = true;
                throw e;
            }
            catch (Exception e)
            {
                await dbTransaction?.RollbackAsync();
                disposeConnection = true;
                // TODO: Create a Transaction exception.
                throw e;
            }
            finally
            {
                await dbTransaction.DisposeAsync();

                if (disposeConnection)
                {
                    await base._dbConnection.CloseAsync();
                    await base._dbConnection.DisposeAsync();
                }
            }
        }

        #endregion WRAPPER METHODS
    }
}
