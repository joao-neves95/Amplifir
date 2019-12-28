using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Data.Common;
using Npgsql;
using Dapper;
using Amplifir.Core.Interfaces;
using Amplifir.Infrastructure.DataAccess.Interfaces;
using Amplifir.Core.Utilities;

namespace Amplifir.Infrastructure.DataAccess
{
    public class DapperDBContext : DBContextBase, IDBContext
    {
        public DapperDBContext()
        {
        }

        public DapperDBContext(string connectionString) : base (connectionString)
        {
            this.OpenDBConnectionAsync( connectionString );
        }

        public async Task<DbConnection> OpenDBConnectionAsync()
        {
            return await this.OpenDBConnectionAsync( StringUtils.BuildConnectionStringWithSSL(
                Environment.GetEnvironmentVariable( "DB_SERVER" ),
                Environment.GetEnvironmentVariable( "DB_PORT" ),
                Environment.GetEnvironmentVariable( "DB_DATABASE" ),
                Environment.GetEnvironmentVariable( "DB_USER" ),
                Environment.GetEnvironmentVariable( "DB_PASSWORD" )
            ) );
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

        public async Task<int> ExecuteTransactionAsync( Dictionary<string, object> sqlAndParameters )
        {
            DbTransaction dbTransaction = null;

            try
            {
                await base._dbConnection.OpenAsync();

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
                throw e;
            }
            catch (System.Exception e)
            {
                await dbTransaction?.RollbackAsync();
                // TODO: Create a Transaction exception.
                throw e;
            }
            finally
            {
                await dbTransaction.DisposeAsync();
                await base._dbConnection.CloseAsync();
            }
        }

        #endregion WRAPPER METHODS
    }
}
