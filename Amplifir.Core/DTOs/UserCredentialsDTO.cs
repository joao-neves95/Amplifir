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
