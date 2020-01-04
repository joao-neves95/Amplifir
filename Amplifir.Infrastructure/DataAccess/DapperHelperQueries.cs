using Amplifir.Core.Entities;

namespace Amplifir.Infrastructure.DataAccess
{
    public static class DapperHelperQueries
    {
        public static string SelectSessionLastInsertedUserId()
        {
            return "SELECT currval( pg_get_serial_sequence('AppUser', 'id') )";
        }

        /// <summary>
        /// 
        /// It's necessary to pass Dapper the "IPv4" parameter.
        /// 
        /// </summary>
        /// <param name="auditLog"></param>
        /// <returns></returns>
        public static string CreateNewLog( string userId, short eventTypeId)
        {
            return $@"INSERT INTO AuditLog (UserId, IPv4, EventTypeId)
                      VALUES ( { userId }, @IPv4, { eventTypeId } )
                   ";
        }
    }
}
