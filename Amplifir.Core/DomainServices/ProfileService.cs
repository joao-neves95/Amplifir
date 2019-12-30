using System;
using System.Collections.Generic;
using System.Text;
using Amplifir.Core.Interfaces;
using Amplifir.Core.Entities;

namespace Amplifir.Core.DomainServices
{
    public class ProfileService
    {
        public ProfileService(IAppUserStore<AppUser, int> appUserStore )
        {
            this._appUserStore = appUserStore;
        }

        private readonly IAppUserStore<AppUser, int> _appUserStore;
    }
}
