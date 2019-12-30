using System;
using System.Collections.Generic;
using System.Text;

namespace Amplifir.Core.Utilities
{
    public static class StringUtils
    {
        // https://www.connectionstrings.com/
        public static string BuildPostreSQLConnectionString(string server, string port, string database, string userName, string password)
        {
            return $"Server={server}; Port={port}; Database={database}; User Id={userName}; Password={password};";
        }

        public static string BuildPostreSQLConnectionStringWithSSL(string server, string port, string database, string userName, string password)
        {
            return StringUtils.BuildPostreSQLConnectionString( server, port, database, userName, password ) + " SslMode=Require;";
        }
    }
}
