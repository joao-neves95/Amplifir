using System.Threading.Tasks;
using Amplifir.Core.Entities;
using Amplifir.Core.Interfaces;

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
