using System;
using System.Collections.Generic;
using System.Text;

namespace Amplifir.Core.Interfaces
{
    public interface ISanitizerService
    {
        string SanitizeHTML( string html );
    }
}
