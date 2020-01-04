using System;
using System.Collections.Generic;
using System.Text;

namespace Amplifir.Core.DTOs
{
    public class ApiResponse<TResponse>
    {
        public bool Error { get; set; }

        public string Message { get; set; }

        public TResponse EndpointResult { get; set; }
    }
}
