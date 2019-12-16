using System.Threading.Tasks;
using System.Data.Common;
using Npgsql;
using Dapper;
using Amplifir.Core.Interfaces;
using Amplifir.Infrastructure.DataAccess.Interfaces;
using System.Collections.Generic;

namespace Amplifir.Infrastructure.DataAccess
{
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
            // TODO: Throw an exeption if the "_connectionString" is null.
            return await this.OpenDBConnectionAsync( this._connectionString );
        }

        public async Task<DbConnection> OpenDBConnectionAsync(string connectionString)
        {
            base._dbConnection = new NpgsqlConnection( connectionString );
            await base._dbConnection.OpenAsync();
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
