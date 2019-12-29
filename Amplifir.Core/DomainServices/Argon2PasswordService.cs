using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Amplifir.Core.Interfaces;
using Amplifir.Core.Utilities;

namespace Amplifir.Core.DomainServices
{
    public class Argon2PasswordService : IPasswordService
    {
        public async Task<string> HashPasswordAsync(string unhashedPassword)
        {
            return await DataHasher.Argon2HashAsync( unhashedPassword );
        }

        public async Task<bool> ValidatePasswordAsync(string hashedPassword, string unhashedPassword)
        {
            return await DataHasher.Argon2CompareAsync( hashedPassword, unhashedPassword );
        }
    }
}
