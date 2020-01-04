using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using Isopoh.Cryptography.Argon2;
using Isopoh.Cryptography.SecureArray;

namespace Amplifir.Core.Utilities
{
    public static class DataHasher
    {
        private static readonly RandomNumberGenerator RNG = RandomNumberGenerator.Create();

        #region Argon2

        private static readonly Argon2Config Argon2Config = new Argon2Config()
        {
            Type = Argon2Type.DataIndependentAddressing,
            Version = Argon2Version.Nineteen,
            TimeCost = 5,
            MemoryCost = 24248,
            Lanes = 5,
            Threads = 2,
            Secret = Encoding.UTF8.GetBytes( DotNetEnv.Env.GetString( "ARGON2_SECRET" ) ),
            HashLength = 32,
            ClearPassword = true
        };

        public static async Task<string> Argon2HashAsync(string data)
        {
            return await Task.Run<string>( () => { return DataHasher.Argon2Hash( data ); } );
        }

        public static string Argon2Hash(string data)
        {
            Argon2Config config = DataHasher.Argon2Config;
            config.Password = Encoding.UTF8.GetBytes( data );

            byte[] salt = new byte[18];
            RNG.GetBytes( salt );
            config.Salt = Encoding.UTF8.GetBytes( System.Convert.ToBase64String( salt ) );

            using Argon2 argon2 = new Argon2( DataHasher.Argon2Config );
            using SecureArray<byte> hash = argon2.Hash();
            return DataHasher.Argon2Config.EncodeString( hash.Buffer );
        }

        public static async Task<bool> Argon2CompareAsync(string hashedData, string unhashedData)
        {
            return await Task.Run<bool>( () => { return DataHasher.Argon2Compare( hashedData, unhashedData ); } );
        }

        public static bool Argon2Compare(string hashedData, string unhashedData)
        {
            return Argon2.Verify( hashedData, unhashedData, DotNetEnv.Env.GetString( "ARGON2_SECRET" ) );
        }

        #endregion Argon2
    }
}
