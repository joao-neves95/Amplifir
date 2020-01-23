/*
 * Copyright (c) 2019 - 2020 João Pedro Martins Neves (SHIVAYL) - All Rights Reserved.
 *
 * Amplifir and all its content is licensed under the GNU Lesser General Public License (LGPL),
 * version 3, located in the root of this project, under the name "LICENSE.md".
 *
 */

using System;
using System.Threading.Tasks;
using Amplifir.Core.Enums;
using Amplifir.Core.Entities;
using Amplifir.Core.Models;
using Amplifir.Core.Utilities;
using Amplifir.Core.Interfaces;

namespace Amplifir.Core.DomainServices
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage( "Reliability", "CA2007:Consider calling ConfigureAwait on the awaited task", Justification = "<Pending>" )]
    public class AuthenticationService : IAuthenticationService
    {
        public AuthenticationService(
            IAppUserStore<AppUser, int> appUserStore, IPasswordService passwordService,
            IAppSettings appSettings,
            IEmailValidatorService emailValidatorService,
            ISanitizerService sanitizerService
        )
        {
            this._appUserStore = appUserStore;
            this._passwordService = passwordService;
            this._appSettings = appSettings;
            this._emailValidatorService = emailValidatorService;
            this._sanitizerService = sanitizerService;
        }

        #region PROPERTIES

        private readonly IAppUserStore<AppUser, int> _appUserStore;

        private readonly IPasswordService _passwordService;

        private readonly IAppSettings _appSettings;

        private readonly IEmailValidatorService _emailValidatorService;

        private readonly ISanitizerService _sanitizerService;

        #endregion PROPERTIES

        public async Task<ValidateSignInResult> ValidateSignInAsync(string email, string password)
        {
            AppUser thisAppUser = await _appUserStore.FindByEmailAsync( email );

            if ( thisAppUser == null || String.IsNullOrEmpty( thisAppUser.Email ) || String.IsNullOrEmpty( thisAppUser.Password ) )
            {
                return new ValidateSignInResult() { State = ValidateSignInState.NotFound, User = null };
            }

            // TODO: Check if the user is locked-out.

            return !await _passwordService.ValidatePasswordAsync( thisAppUser.Password, password ) ?
                new ValidateSignInResult() { State = ValidateSignInState.WrongPassword, User = null } :
                new ValidateSignInResult() { State = ValidateSignInState.Success, User = thisAppUser };
        }

        public async Task<RegisterUserResult> RegisterUserAsync(IAppUser appUser)
        {
            RegisterUserResult registerUserResult = new RegisterUserResult();
            registerUserResult.User = null;

            if (!String.IsNullOrEmpty( appUser.Password ) && appUser.Password.Length < _appSettings.Password_MinLength)
            {
                registerUserResult.State = RegisterUserState.PasswordTooSmall;
                return registerUserResult;
            }

            if (String.IsNullOrEmpty( appUser.Email ) || !this._emailValidatorService.IsValid( appUser.Email ))
            {
                registerUserResult.State = RegisterUserState.InvalidEmail;
                return registerUserResult;
            }

            // TODO: Check if it's a temporary email or a spam email.

            if (await _appUserStore.EmailExistsAsync( appUser.Email ))
            {
                registerUserResult.State = RegisterUserState.EmailExists;
                return registerUserResult;
            }

            appUser.UserName = StringUtils.GenerateRandomString( 8 );
            appUser.Email = this._sanitizerService.SanitizeHTML( appUser.Email );

            appUser.Password = await _passwordService.HashPasswordAsync( appUser.Password );
            await _appUserStore.CreateAsync( appUser as AppUser );
            appUser.Id = await _appUserStore.GetLastInsertedUserId();

            registerUserResult.State = RegisterUserState.Success;
            registerUserResult.User = appUser;
            return registerUserResult;
        }
    }
}
