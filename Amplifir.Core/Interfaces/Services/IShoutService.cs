using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Amplifir.Core.Enums;
using Amplifir.Core.Entities;

namespace Amplifir.Core.Interfaces
{
    public interface IShoutService
    {
        Task<CreateShoutState> CreateAsync( Shout newShout );

        Task DeleteAsync( int shoutId, int userId );
    }
}
