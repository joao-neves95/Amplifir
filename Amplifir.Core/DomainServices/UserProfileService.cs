using System;
using System.Collections.Generic;
using System.Text;
using Amplifir.Core.Interfaces;
using Amplifir.Core.Entities;
using System.Threading.Tasks;

namespace Amplifir.Core.DomainServices
{
    public class UserProfileService : IUserProfileService
    {
        public UserProfileService(IAppUserProfileStore appUserProfileStore)
        {
            this._appUserProfileStore = appUserProfileStore;
        }

        private readonly IAppUserProfileStore _appUserProfileStore;

        public async Task<AppUserProfile> GetByUserIdAsync(int userId)
        {
            return await _appUserProfileStore.GetByUserIdAsync( userId );
        }
    }
}
