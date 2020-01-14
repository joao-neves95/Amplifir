/*
 * Copyright (c) 2019 - 2020 Jo�o Pedro Martins Neves (SHIVAYL) - All Rights Reserved.
 *
 * Amplifir and all its content is licensed under the GNU Lesser General Public License (LGPL),
 * version 3, located in the root of this project, under the name "LICENSE.md".
 *
 */

using Amplifir.Core.Enums;
using Amplifir.Core.Interfaces;

namespace Amplifir.Core.Models
{
    public class RegisterUserResult
    {
        public RegisterUserState State { get; set; }

        public IAppUser User { get; set; }
    }
}
