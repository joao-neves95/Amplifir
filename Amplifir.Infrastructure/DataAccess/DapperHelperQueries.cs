using Amplifir.Core.Entities;

namespace Amplifir.Infrastructure.DataAccess
{
    public static class DapperHelperQueries
    {
        public static string SelectSessionLastInsertedUserId()
        {
            return "SELECT currval( pg_get_serial_sequence('AppUser', 'id') )";
        }

        public static string SelectSessionLastInsertedShoutId()
        {
            return "SELECT currval( pg_get_serial_sequence('Shout', 'id') )";
        }

        /// <summary>
        /// 
        /// You have to pass the value you want to search for to Dapper
        ///   on the parameter object with the name "IPv4".
        ///     
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="eventTypeId"></param>
        /// <returns></returns>
        public static string CreateNewLog(string userId, short eventTypeId)
        {
            return $@"INSERT INTO AuditLog (UserId, IPv4, EventTypeId)
                      VALUES ( { userId }, @IPv4, { eventTypeId } )
                   ";
        }

        /// <summary>
        /// <para>
        /// 
        /// You have to pass the value you want to search for to Dapper
        ///   on the parameter object with the name "Value".
        ///   
        /// </para>
        /// <para>
        /// 
        /// The query returns 0 or 1.
        ///   
        /// </para>
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="collumnName"></param>
        /// <returns></returns>
        public static string Exists(string tableName, string collumnName)
        {
            // COALESCE() is the PostgreSQL's similar function to ISNULL() in SQLServer.
            return $@"SELECT COALESCE(
                          (
                               SELECT 1
                               FROM {tableName}
                               WHERE {collumnName} = @Value
                          ),
                          0
                      )";
        }
    }
}
