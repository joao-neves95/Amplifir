using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Amplifir.Core.Interfaces
{
    public interface IPasswordService
    {
        Task<string> HashPasswordAsync( string unhashedPassword );

        Task<bool> ValidatePasswordAsync( string hashedPassword, string unhashedPassword );
    }
}
