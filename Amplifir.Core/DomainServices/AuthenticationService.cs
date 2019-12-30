using System;
using System.Threading.Tasks;
using Amplifir.Core.Enums;
using Amplifir.Core.Interfaces;
using Amplifir.Core.Entities;
using Amplifir.Core.Models;

namespace Amplifir.Core.DomainServices
{
    public class AuthenticationService : IAuthenticationService
    {
        public AuthenticationService(IAppUserStore<AppUser, int> appUserStore, IPasswordService passwordService)
        {
            this._appUserStore = appUserStore;
            this._passwordService = passwordService;
        }

        private readonly IAppUserStore<AppUser, int> _appUserStore;

        private readonly IPasswordService _passwordService;

        public async Task<ValidateSignInResult> ValidateSignInAsync(string email, string password)
        {
            // TODO: (DEBUG) Check result in case the user does not exist.
            AppUser thisAppUser = await _appUserStore.FindByEmailAsync( email );

            if ( thisAppUser == null || String.IsNullOrEmpty( thisAppUser.Email ) || String.IsNullOrEmpty( thisAppUser.Password ) )
            {
                return new ValidateSignInResult() { State = ValidateSignInState.NotFound, User = null };
            }

            // TODO: Check if the user is locked-out.

            return !await _passwordService.ValidatePasswordAsync( thisAppUser.Password, password ) ?
                new ValidateSignInResult() { State = ValidateSignInState.InvalidPassword, User = null } :
                new ValidateSignInResult() { State = ValidateSignInState.Success, User = thisAppUser };
        }

        public async Task<RegisterUserResult> RegisterUserAsync(string email, string password)
        {
            if (!String.IsNullOrEmpty( password ) && password.Length < 8)
            {
                return new RegisterUserResult() { State = RegisterUserState.PasswordTooSmall, User = null };
            }

            // TODO: Check if it's a valid email.
            // TODO: Check if it's a temporary email or a spam email.

            if (!String.IsNullOrEmpty( email ) && await _appUserStore.EmailExists( email ))
            {
                return new RegisterUserResult() { State = RegisterUserState.EmailExists, User = null };
            }

            AppUser appUser = new AppUser()
            {
                Email = email,
                Password = await _passwordService.HashPasswordAsync( password )
            };

            // This call can return an Exception from the DataAccess layer.
            await _appUserStore.CreateAsync( appUser );
            appUser.Id = await _appUserStore.GetLastInsertedUserId();

            return new RegisterUserResult() { State = RegisterUserState.Success, User = appUser };
        }
    }
}
