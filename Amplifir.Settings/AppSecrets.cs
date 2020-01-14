/*
 * Copyright (c) 2019 - 2020 João Pedro Martins Neves (SHIVAYL) - All Rights Reserved.
 *
 * Amplifir and all its content is licensed under the GNU Lesser General Public License (LGPL),
 * version 3, located in the root of this project, under the name "LICENSE.md".
 *
 */

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
