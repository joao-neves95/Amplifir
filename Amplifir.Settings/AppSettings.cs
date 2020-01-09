using System;
using Amplifir.Core.Interfaces;

namespace Amplifir.Settings
{
    public class AppSettings : IAppSettings
    {
        public int Shout_MaxLength => Convert.ToInt32( _AppSettings.Shout_MaxLength );

        public int Password_MinLength => Convert.ToInt32( _AppSettings.Password_MinLength );
    }
}
