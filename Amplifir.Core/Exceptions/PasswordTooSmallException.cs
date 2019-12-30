using System;
using System.Collections.Generic;
using System.Text;

namespace Amplifir.Core.Exceptions
{
    public class PasswordTooSmallException : Exception
    {
        public PasswordTooSmallException() : base()
        {
        }

        public PasswordTooSmallException(string message) : base( message )
        {
        }

        public PasswordTooSmallException(string message, Exception innerException) : base( message, innerException )
        {
        }
    }
}
