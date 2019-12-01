using System.Threading.Tasks;
using System.Data.Common;
using Npgsql;
using Amplifir.Core.Interfaces;
using Amplifir.Infrastructure.DataAccess.Interfaces;

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
    }
}
