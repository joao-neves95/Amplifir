/*
 * Copyright (c) 2019 - 2020 João Pedro Martins Neves (SHIVAYL) - All Rights Reserved.
 *
 * Amplifir and all its content is licensed under the GNU Lesser General Public License (LGPL),
 * version 3, located in the root of this project, under the name "LICENSE.md".
 *
 */

using System;

namespace Amplifir.Core.Exceptions
{
    public class EmailExistsException : Exception
    {
        public EmailExistsException() : base()
        {
        }

        public EmailExistsException(string message) : base( message )
        {
        }

        public EmailExistsException(string message, Exception innerException) : base( message, innerException )
        {
        }
    }
}
