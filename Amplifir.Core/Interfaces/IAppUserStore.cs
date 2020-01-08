using System.Threading.Tasks;
using Microsoft.AspNet.Identity;

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
        Task<int> GetLastInsertedUserId();

        Task<bool> EmailExistsAsync( string email );
    }
}
