using System;
using System.Collections.Generic;
using System.Text;

namespace Amplifir.Infrastructure.DataAccess
{
    public static class DapperHelperQueries
    {
        public static string SelectLastInsertedUserId()
        {
            return "SELECT currval( pg_get_serial_sequence('AppUser', 'Id') )";
        }
    }
}
