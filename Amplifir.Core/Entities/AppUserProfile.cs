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

namespace Amplifir.Core.Entities
{
    public class AppUserProfile
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public string Bio { get; set; }

        public string Website { get; set; }

        public int FollowingCount { get; set; }

        public int FollowersCount { get; set; }

        public string Userlocation { get; set; }

        public DateTime BirthDate { get; set; }
    }
}
