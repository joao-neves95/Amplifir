/*
 * Copyright (c) 2019 - 2020 João Pedro Martins Neves (SHIVAYL) - All Rights Reserved.
 *
 * Amplifir and all its content is licensed under the GNU Lesser General Public License (LGPL),
 * version 3, located in the root of this project, under the name "LICENSE.md".
 *
 */

using System;
using Amplifir.Core.Interfaces;

namespace Amplifir.Settings
{
    public class AppSettings : IAppSettings
    {
        public int Shout_MaxLength => Convert.ToInt32( _AppSettings.Shout_MaxLength );

        public int Shout_MinLength => Convert.ToInt32( _AppSettings.Shout_MinLength );

        public int Password_MinLength => Convert.ToInt32( _AppSettings.Password_MinLength );
    }
}
