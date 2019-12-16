using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNet.Identity;

namespace Amplifir.Core.Entities
{
    public class AppUser : IUser<int>
    {
        public int Id { get; set; }

        public string UserName { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public string PhoneNumber { get; set; }
    }
}
