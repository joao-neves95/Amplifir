using System;
using Amplifir.Core.Interfaces;

namespace Amplifir.UI.Web.AppSettings
{
    public class AppSettings : IAppSettings
    {
        public int Shout_MaxLength => Convert.ToInt32( _AppSettings.Shout_MaxLength );
    }
}
