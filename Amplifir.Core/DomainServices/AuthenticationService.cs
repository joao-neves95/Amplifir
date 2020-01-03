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

        public async Task<RegisterUserResult> RegisterUserAsync(IAppUser appUser)
        {
            if (!String.IsNullOrEmpty( appUser.Password ) && appUser.Password.Length < 8)
            {
                return new RegisterUserResult() { State = RegisterUserState.PasswordTooSmall, User = null };
            }

            // TODO: Check if it's a valid email.
            // TODO: Check if it's a temporary email or a spam email.

            if (!String.IsNullOrEmpty( appUser.Email ) && await _appUserStore.EmailExists( appUser.Email ))
            {
                return new RegisterUserResult() { State = RegisterUserState.EmailExists, User = null };
            }

            appUser.Password = await _passwordService.HashPasswordAsync( appUser.Password );

            // This call can return an Exception from the DataAccess layer.
            await _appUserStore.CreateAsync( appUser as AppUser );
            appUser.Id = await _appUserStore.GetLastInsertedUserId();

            return new RegisterUserResult() { State = RegisterUserState.Success, User = appUser };
        }
    }
}
