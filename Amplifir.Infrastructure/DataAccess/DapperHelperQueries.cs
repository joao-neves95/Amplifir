using System;
using System.Collections.Generic;
using System.Text;

namespace Amplifir.Infrastructure.DataAccess
{
    public static class DapperHelperQueries
    {
        public static string SelectSessionLastInsertedUserId()
        {
            return "SELECT currval( pg_get_serial_sequence('AppUser', 'id') )";
        }
    }
}
