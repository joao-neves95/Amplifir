using System;
using System.Collections.Generic;
using System.Text;

namespace Amplifir.Core.Utilities
{
    public static class StringUtils
    {
        // https://www.connectionstrings.com/
        public static string BuildConnectionString(string server, string port, string database, string userName, string password)
        {
            return $"Server={server}; Port={port}; Database={database}; User Id={userName}; Password={password};";
        }

        public static string BuildConnectionStringWithSSL(string server, string port, string database, string userName, string password)
        {
            return StringUtils.BuildConnectionString( server, port, database, userName, password ) + " SslMode=Require;";
        }
    }
}
