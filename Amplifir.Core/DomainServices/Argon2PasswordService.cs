/*
 * Copyright (c) 2019 - 2020 João Pedro Martins Neves (SHIVAYL) - All Rights Reserved.
 *
 * Amplifir and all its content is licensed under the GNU Lesser General Public License (LGPL),
 * version 3, located in the root of this project, under the name "LICENSE.md".
 *
 */

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

        public bool ValidatePassword(string hashedPassword, string unhashedPassword)
        {
            return DataHasher.Argon2Compare( hashedPassword, unhashedPassword );
        }

        public async Task<bool> ValidatePasswordAsync(string hashedPassword, string unhashedPassword)
        {
            return await DataHasher.Argon2CompareAsync( hashedPassword, unhashedPassword );
        }
    }
}
