/*
 * Copyright (c) 2019 - 2020 João Pedro Martins Neves (SHIVAYL) - All Rights Reserved.
 *
 * Amplifir and all its content is licensed under the GNU Lesser General Public License (LGPL),
 * version 3, located in the root of this project, under the name "LICENSE.md".
 *
 */

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
