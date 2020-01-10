using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Amplifir.Core.Entities;

namespace Amplifir.Core.Interfaces
{
    public interface IAppUserProfileStore
    {
        Task<AppUserProfile> GetByUserIdAsync( int userId );
    }
}
