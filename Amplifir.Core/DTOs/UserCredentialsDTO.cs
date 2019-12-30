using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Amplifir.Core.DTOs
{
    public class UserCredentialsDTO
    {
        public UserCredentialsDTO()
        {
        }

        public UserCredentialsDTO( string email, string password )
        {
            this.Email = email;
            this.Password = password;
        }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
