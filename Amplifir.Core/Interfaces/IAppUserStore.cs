using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNet.Identity;
using Amplifir.Infrastructure.Entities;

namespace Amplifir.Core.Interfaces
{
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
