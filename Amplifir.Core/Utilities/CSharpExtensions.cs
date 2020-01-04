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
