using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNet.Identity;

namespace Amplifir.Infrastructure.Entities
{
    public class AppUser : IUser<int>
    {
        public int Id { get; set; }

        public string UserName { get; set; }
    }
}
