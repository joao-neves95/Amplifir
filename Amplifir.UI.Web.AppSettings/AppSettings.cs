using Amplifir.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Amplifir.UI.Web.AppSettings
{
    public class AppSettings : IAppSettings
    {
        public int Shout_MaxLength => Convert.ToInt32( _AppSettings.Shout_MaxLength );
    }
}
