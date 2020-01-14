/*
 * Copyright (c) 2019 - 2020 João Pedro Martins Neves (SHIVAYL) - All Rights Reserved.
 *
 * Amplifir and all its content is licensed under the GNU Lesser General Public License (LGPL),
 * version 3, located in the root of this project, under the name "LICENSE.md".
 *
 */

using System;
using System.Collections.Generic;
using Amplifir.Core.DTOs;

namespace Amplifir.Core.Entities
{
    public class Shout : NewShoutDTO
    {
        public int Id { get; set; }

        public DateTime CreateDate { get; set; }

        public int LikesCount { get; set; }

        public int DislikesCount { get; set; }

        public List<string> Hashtags { get; set; }

        public List<ShoutAsset> Assets { get; set; }
    }
}
