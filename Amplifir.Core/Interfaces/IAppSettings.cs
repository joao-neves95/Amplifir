using System;
using System.Collections.Generic;
using System.Text;

namespace Amplifir.Core.Interfaces
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage( "Naming", "CA1707:Identifiers should not contain underscores", Justification = "<Pending>" )]
    public interface IAppSettings
    {
        int Shout_MaxLength { get; }

        int Password_MinLength { get; }
    }
}
