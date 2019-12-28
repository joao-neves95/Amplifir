using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Data.Common;
using Dapper;
using Amplifir.Core.Interfaces;
using Amplifir.Core.Models;

namespace Amplifir.Infrastructure.DataAccess
{
    /// <summary>
    /// 
    /// Used for the application's user Identity.
    /// This must not be delivered to the API. Its intent is to be used internally.
    /// 
    /// </summary>
    public class AppUserDapperStore : DBStoreBase, IAppUserStore<AppUser, int>
    {
        public AppUserDapperStore(IDBContext dBContext) : base(dBContext)
        {
        }

        public async Task CreateAsync(AppUser user)
        {
            await base._dBContext.ExecuteTransactionAsync( new Dictionary<string, object>()
            {
                {
                    @"INSERT INTO AppUser (UserName, Email, Password)
                      VALUES (@UserName, @Email, @Password)
                    ",
                    new { UserName = user.UserName, Email = user.Email, Password = user.Password }
                },
                {
                    $@"INSERT INTO AppUserProfile (UserId)
                       VALUES ( { HelperQueries.SelectLastInsertedUserId() } )
                    ",
                    null
                },
                {
                    $@"INSERT INTO AuditLog (UserId, IPv4, EventTypeId)
                       VALUES ( { HelperQueries.SelectLastInsertedUserId() }, @IPv4, { EventTypeId.Register } )
                    ",
                    new { IPv4 = user.Ipv4 }
                }
            } );

            throw new NotImplementedException();
        }

        public async Task DeleteAsync(AppUser user)
        {
            await base._dBContext.ExecuteTransactionAsync( new Dictionary<string, object>()
            {
                {
                    @"DELETE FROM AppUser
                      WHERE Id = @Id
                    ",
                    new { Id = user.Id }
                },
                {
                    @"DELETE FROM AppUserProfile
                      WHERE UserId = @UserId
                    ",
                    new { UserId = user.Id }
                }
            } );

            throw new NotImplementedException();
        }

        public async Task<bool> EmailExists(string email)
        {
            await base._dBContext.OpenDBConnectionAsync();

            using (base._dBContext.DbConnection)
            {
                return await base._dBContext.DbConnection.ExecuteScalarAsync<int>(
                    // COALESCE() is the PostgreSQL's similar function to ISNULL() in SQLServer.
                    @"SELECT COALESCE(
                        (SELECT 1
                         FROM AppUser
                         WHERE Email = @Email
                        ), 0
                    ",
                    new { @Email = email }

                ) == 1;
            }
        }

        public async Task<AppUser> FindByEmailAsync(string email)
        {
            await base._dBContext.OpenDBConnectionAsync();

            using (base._dBContext.DbConnection)
            {
                return await base._dBContext.DbConnection.QueryFirstOrDefaultAsync<AppUser>(
                    @"SELECT Id, Password, PhoneNumber
                      FROM AppUser
                      WHERE Email = @Email
                    ",
                    new { Email = email }
                );
            }
        }

        public async Task<AppUser> FindByIdAsync(int userId)
        {
            await base._dBContext.OpenDBConnectionAsync();

            using (base._dBContext.DbConnection)
            {
                return await base._dBContext.DbConnection.QueryFirstOrDefaultAsync<AppUser>(
                    @"SELECT Id, Email, Password, PhoneNumber
                      FROM AppUser
                      WHERE Id = @Id
                    ",
                    new { Id = userId }
                );
            }
        }

        public Task<AppUser> FindByNameAsync(string userName)
        {
            throw new NotImplementedException();
        }

        public Task<int> GetAccessFailedCountAsync(AppUser user)
        {
            throw new NotImplementedException();
        }

        public async Task<string> GetEmailAsync(AppUser user)
        {
            await base._dBContext.OpenDBConnectionAsync();

            using (base._dBContext.DbConnection)
            {
                return await base._dBContext.DbConnection.QueryFirstOrDefaultAsync<string>(
                    @"SELECT Email
                      FROM AppUser
                      WHERE Id = @Id
                    ",
                    new { Id = user.Id }
                );
            }
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
