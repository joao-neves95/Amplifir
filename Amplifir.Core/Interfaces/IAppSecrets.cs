using System;
using System.Collections.Generic;
using System.Text;

namespace Amplifir.Core.Interfaces
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage( "Naming", "CA1707:Identifiers should not contain underscores", Justification = "<Pending>" )]
    public interface IAppSecrets
    {
        string DB_Server { get; }

        string DB_Port { get; }

        string DB_DatabaseName { get; }

        string DB_User { get; }

        string DB_Password { get; }

        string JWT_Issuer { get; }

        int JWT_ExpirationDays { get; }

        string JWT_Key { get; }
    }
}
