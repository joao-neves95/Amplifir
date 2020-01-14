/*
 * Copyright (c) 2019 - 2020 João Pedro Martins Neves (SHIVAYL) - All Rights Reserved.
 *
 * Amplifir and all its content is licensed under the GNU Lesser General Public License (LGPL),
 * version 3, located in the root of this project, under the name "LICENSE.md".
 *
 */

using System.Threading.Tasks;
using Microsoft.AspNet.Identity;

namespace Amplifir.Core.Interfaces
{
    /// <summary>
    /// <para>
    /// 
    /// Used for the application's user Identity and management.
    /// 
    /// </para>
    /// <para>
    /// 
    /// This must not be used directly from the API, only to be used internally.
    /// Must be encapsulated within a service (façade) with the business logic
    /// and consumed from there.
    /// 
    /// </para>
    /// </summary>
    public interface IAppUserStore<TUser, TKey> :
        IUserStore<TUser, TKey>,
        IUserPasswordStore<TUser, TKey>,
        IUserEmailStore<TUser, TKey>,
        IUserPhoneNumberStore<TUser, TKey>,
        IUserLockoutStore<TUser, TKey>

        where TUser : class, IUser<TKey>
    {
        Task<int> GetLastInsertedUserId();

        Task<bool> EmailExistsAsync( string email );
    }
}
