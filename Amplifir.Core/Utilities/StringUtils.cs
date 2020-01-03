using System;
using System.Collections.Generic;
using System.Security.Cryptography;
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

        /// <summary>
        /// The characters were mixed to provide more randomness.
        /// </summary>
        private const string ALLOWED_RANDOM_CHARACTERS = "AIN5YPk9qepTznjOM27vSiULtK98JgFdHw3Zm_5lx6EDGa1sB-2WrVR3ChQ4f168yoc74Xub";

        /// <summary>
        /// 
        /// Generate a cryptographically secure random string of characters.
        /// 
        /// </summary>
        /// <returns></returns>
        public static string GenerateRandomString( int size )
        {
            StringBuilder strBuilder = new StringBuilder( size );
            using RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
            byte[] randomNumber = new byte[sizeof( uint )];

            for (int i = 0; i < size; ++i)
            {
                rng.GetBytes( randomNumber );
                strBuilder.Append( StringUtils.ALLOWED_RANDOM_CHARACTERS[Math.Abs( BitConverter.ToInt32( randomNumber, 0 ) ) % ALLOWED_RANDOM_CHARACTERS.Length] );
            }

            return strBuilder.ToString();
        }
    }
}
