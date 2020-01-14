/*
 * Copyright (c) 2019 - 2020 João Pedro Martins Neves (SHIVAYL) - All Rights Reserved.
 *
 * Amplifir and all its content is licensed under the GNU Lesser General Public License (LGPL),
 * version 3, located in the root of this project, under the name "LICENSE.md".
 *
 */

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
