using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNet.Identity;
using Amplifir;
using Amplifir.Infrastructure.Entities;

namespace Amplifir.Infrastructure.DataAccess
{
    public class AppUserDapperContext : DapperDBContext,
        IUserStore<AppUser, int>,
        IUserPasswordStore<AppUser, int>,
        IUserEmailStore<AppUser, int>,
        IUserPhoneNumberStore<AppUser, int>,
        IUserLockoutStore<AppUser, int>
    {
        public Task CreateAsync(AppUser user)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(AppUser user)
        {
            throw new NotImplementedException();
        }

        public Task<AppUser> FindByEmailAsync(string email)
        {
            throw new NotImplementedException();
        }

        public Task<AppUser> FindByIdAsync(int userId)
        {
            throw new NotImplementedException();
        }

        public Task<AppUser> FindByIdAsync(string userId)
        {
            throw new NotImplementedException();
        }

        public Task<AppUser> FindByNameAsync(string userName)
        {
            throw new NotImplementedException();
        }

        public Task<int> GetAccessFailedCountAsync(AppUser user)
        {
            throw new NotImplementedException();
        }

        public Task<string> GetEmailAsync(AppUser user)
        {
            throw new NotImplementedException();
        }

        public Task<bool> GetEmailConfirmedAsync(AppUser user)
        {
            throw new NotImplementedException();
        }

        public Task<bool> GetLockoutEnabledAsync(AppUser user)
        {
            throw new NotImplementedException();
        }

        public Task<DateTimeOffset> GetLockoutEndDateAsync(AppUser user)
        {
            throw new NotImplementedException();
        }

        public Task<string> GetPasswordHashAsync(AppUser user)
        {
            throw new NotImplementedException();
        }

        public Task<string> GetPhoneNumberAsync(AppUser user)
        {
            throw new NotImplementedException();
        }

        public Task<bool> GetPhoneNumberConfirmedAsync(AppUser user)
        {
            throw new NotImplementedException();
        }

        public Task<bool> HasPasswordAsync(AppUser user)
        {
            throw new NotImplementedException();
        }

        public Task<int> IncrementAccessFailedCountAsync(AppUser user)
        {
            throw new NotImplementedException();
        }

        public Task ResetAccessFailedCountAsync(AppUser user)
        {
            throw new NotImplementedException();
        }

        public Task SetEmailAsync(AppUser user, string email)
        {
            throw new NotImplementedException();
        }

        public Task SetEmailConfirmedAsync(AppUser user, bool confirmed)
        {
            throw new NotImplementedException();
        }

        public Task SetLockoutEnabledAsync(AppUser user, bool enabled)
        {
            throw new NotImplementedException();
        }

        public Task SetLockoutEndDateAsync(AppUser user, DateTimeOffset lockoutEnd)
        {
            throw new NotImplementedException();
        }

        public Task SetPasswordHashAsync(AppUser user, string passwordHash)
        {
            throw new NotImplementedException();
        }

        public Task SetPhoneNumberAsync(AppUser user, string phoneNumber)
        {
            throw new NotImplementedException();
        }

        public Task SetPhoneNumberConfirmedAsync(AppUser user, bool confirmed)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(AppUser user)
        {
            throw new NotImplementedException();
        }
    }
}
