using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Amplifir.Core.Interfaces;

namespace Amplifir.Settings
{
    public class AppSecrets : IAppSecrets
    {
        public string DB_Server => DotNetEnv.Env.GetString( "DB_SERVER" );

        public string DB_Port => DotNetEnv.Env.GetString( "DB_PORT" );

        public string DB_DatabaseName => DotNetEnv.Env.GetString( "DB_DATABASE" );

        public string DB_User => DotNetEnv.Env.GetString( "DB_USER" );

        public string DB_Password => DotNetEnv.Env.GetString( "DB_PASSWORD" );

        public string JWT_Issuer => DotNetEnv.Env.GetString( "JWT_ISSUER" );

        public int JWT_ExpirationDays => DotNetEnv.Env.GetInt( "JWT_EXPIRATION_DAYS" );

        public string JWT_Key => DotNetEnv.Env.GetString( "JWT_KEY" );
    }
}
