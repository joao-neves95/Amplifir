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

namespace Amplifir.Core.DTOs
{
    public class NewShoutDTO
    {
        /// <summary>
        ///
        /// This property is ignored by the server.
        /// 
        /// </summary>
        public int UserId { get; set; }

        public string Content { get; set; }
    }
}
