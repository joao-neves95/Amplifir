using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Amplifir.Core.Enums;
using Amplifir.Core.Exceptions;
using Amplifir.Core.Interfaces;
using Amplifir.Core.Models;
using Amplifir.Core.Utilities;

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
                return ValidateSignInResult.NotFound;
            }

            // TODO: Check if the user is locked-out.

            return !await _passwordService.ValidatePasswordAsync( thisAppUser.Password, password ) ?
                ValidateSignInResult.InvalidPassword :
                ValidateSignInResult.Success;
        }

        public async Task<RegisterUserResult> RegisterUserAsync(string email, string password)
        {
            if ( !String.IsNullOrEmpty( password ) && password.Length < 8 )
            {
                return RegisterUserResult.PasswordTooSmall;
            }

            // TODO: Check if it's a valid email.
            // TODO: Check if it's a temporary email or a spam email.

            if ( !String.IsNullOrEmpty(email) && await _appUserStore.EmailExists( email ) )
            {
                return RegisterUserResult.EmailExists;
            }

            // This call can return an Exception from the DataAccess layer.
            await _appUserStore.CreateAsync( new AppUser()
            {
                Email = email,
                Password = await _passwordService.HashPasswordAsync( password )
            } );

            return RegisterUserResult.Success;
        }
    }
}
