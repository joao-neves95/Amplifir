/*
 * Copyright (c) 2019 - 2020 Jo�o Pedro Martins Neves (SHIVAYL) - All Rights Reserved.
 *
 * Amplifir and all its content is licensed under the GNU Lesser General Public License (LGPL),
 * version 3, located in the root of this project, under the name "LICENSE.md".
 *
 */

using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using Dapper;
using Amplifir.Core.Entities;
using Amplifir.Core.Interfaces;

namespace Amplifir.Infrastructure.DataAccess.Stores
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage( "Reliability", "CA2007:Consider calling ConfigureAwait on the awaited task", Justification = "<Pending>" )]
    public class AppUserDapperStore : DBStoreBase, IAppUserStore<AppUser, int>
    {
        public AppUserDapperStore(IDBContext dBContext) : base( dBContext )
        {
        }

        public async Task CreateAsync(AppUser user)
        {
            await base._dBContext.ExecuteTransactionAsync( new Dictionary<string, object>()
            {
                {
                    @"INSERT INTO AppUser (UserName, Email, Password)
                      VALUES (@UserName, @Email, @Password);
                    ",
                    new { UserName = user.UserName, Email = user.Email, Password = user.Password }
                },
                {
                    $@"INSERT INTO AppUserProfile (UserId)
                       VALUES ( ( { DapperHelperQueries.SelectSessionLastInsertedUserId() } ) );
                    ",
                    null
                },
                {
                    DapperHelperQueries.CreateNewLog( $"( {DapperHelperQueries.SelectSessionLastInsertedUserId()} )", EventTypeId.Register ),
                    new { IPv4 = user.Ipv4 }
                }

            }, false );
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
        }

        public async Task<bool> EmailExistsAsync(string email)
        {
            await base._dBContext.OpenDBConnectionAsync();

            using (base._dBContext.DbConnection)
            {
                return await base._dBContext.DbConnection.ExecuteScalarAsync<int>(
                    DapperHelperQueries.Exists( "AppUser", "Email" ),
                    new { Value1 = email }

                ) == 1;
            }
        }

        public async Task<AppUser> FindByEmailAsync(string email)
        {
            await base._dBContext.OpenDBConnectionAsync();

            using (base._dBContext.DbConnection)
            {
                return await base._dBContext.DbConnection.QueryFirstOrDefaultAsync<AppUser>(
                    @"SELECT Id, Email, Password
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
                    @"SELECT Id, Email, Password
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

        public async Task<int> GetLastInsertedUserId()
        {
            await base._dBContext.OpenDBConnectionAsync();

            using (base._dBContext.DbConnection)
            {
                return await base._dBContext.DbConnection.ExecuteScalarAsync<int>( DapperHelperQueries.SelectSessionLastInsertedUserId() );
            }
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
