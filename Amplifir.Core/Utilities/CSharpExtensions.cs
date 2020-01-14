/*
 * Copyright (c) 2019 - 2020 João Pedro Martins Neves (SHIVAYL) - All Rights Reserved.
 *
 * Amplifir and all its content is licensed under the GNU Lesser General Public License (LGPL),
 * version 3, located in the root of this project, under the name "LICENSE.md".
 *
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Amplifir.Core.Utilities
{
    public static class CSharpExtensions
    {
        public static TReturn Switch<TCase, TReturn>(this TCase @case, Dictionary<TCase, Func<TReturn>> actions, Func<TReturn> defaultFunc)
        {
            if (actions.TryGetValue( @case, out Func<TReturn> action ))
            {
                return action();
            }
            else
            {
                return defaultFunc();
            }
        }
    }
}
