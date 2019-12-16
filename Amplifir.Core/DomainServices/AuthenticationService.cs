using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Amplifir.Core.Interfaces;
using Amplifir.Core.Entities;

namespace Amplifir.Core.DomainServices
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IAppUserStore<AppUser, int> _appUserStore;

        public AuthenticationService(IAppUserStore<AppUser, int> appUserStore)
        {
            this._appUserStore = appUserStore;
        }

        public Task LoginUser(string email, string password)
        {
            throw new NotImplementedException();
        }

        public Task RegisterUser(string email, string password)
        {
            // 1 - Check if the email already exists.
            // 2 - Check the password's minimun length.
            // 3 - Create user.
            throw new NotImplementedException();
        }
    }
}
