using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNet.Identity;
using Amplifir.Infrastructure.Entities;

namespace Amplifir.Core.Interfaces
{
    /// <summary>
    /// 
    /// Used for the application's user Identity.
    /// This must not be delivered to the API. Its intent is to be used internally.
    /// 
    /// </summary>
    public interface IAppUserStore<TUser, TKey> :
        IUserStore<TUser, TKey>,
        IUserPasswordStore<TUser, TKey>,
        IUserEmailStore<TUser, TKey>,
        IUserPhoneNumberStore<TUser, TKey>,
        IUserLockoutStore<TUser, TKey>

        where TUser : class, IUser<TKey>
    {
    }
}
