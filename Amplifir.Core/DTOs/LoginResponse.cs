/*
 * Copyright (c) 2019 - 2020 João Pedro Martins Neves (SHIVAYL) - All Rights Reserved.
 *
 * Amplifir and all its content is licensed under the GNU Lesser General Public License (LGPL),
 * version 3, located in the root of this project, under the name "LICENSE.md".
 *
 */

namespace Amplifir.Core.DTOs
{
    public class LoginResponse
    {
        public LoginResponse()
        {
        }

        public LoginResponse(string jwt)
        {
            this.JWT = jwt;
        }

        public string JWT { get; set; }
    }
}
