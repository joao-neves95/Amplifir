using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Amplifir.Core.Interfaces;
using Amplifir.Core.Entities;
using Amplifir.Core.Utilities;

namespace Amplifir.Core.DomainServices
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IAppUserStore<AppUser, int> _appUserStore;

        public AuthenticationService(IAppUserStore<AppUser, int> appUserStore)
        {
            this._appUserStore = appUserStore;
        }

        public async Task LoginUser(string email, string password)
        {
            throw new NotImplementedException();
        }

        public async Task RegisterUser(string email, string password)
        {
            try
            {
                if (password.Length < 8)
                {
                    // PasswordTooSmallException.
                }

                if (await _appUserStore.EmailExists( email ))
                {
                    // EmailExistsException.
                }

                await _appUserStore.CreateAsync( new AppUser()
                {
                    Email = email,
                    Password = await DataHasher.Argon2HashAsync( password )
                } );
            }
            catch (Exception e)
            {
                // 500 Error.
            }

            throw new NotImplementedException();
        }
    }
}
