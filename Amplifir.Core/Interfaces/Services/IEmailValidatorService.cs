using System;
using System.Collections.Generic;
using System.Text;

namespace Amplifir.Core.Interfaces
{
    public interface IEmailValidatorService
    {
        bool IsValid( string email );
    }
}
