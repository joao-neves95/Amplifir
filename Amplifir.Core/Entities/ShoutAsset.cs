/*
 * Copyright (c) 2019 - 2020 João Pedro Martins Neves (SHIVAYL) - All Rights Reserved.
 *
 * Amplifir and all its content is licensed under the GNU Lesser General Public License (LGPL),
 * version 3, located in the root of this project, under the name "LICENSE.md".
 *
 */

namespace Amplifir.Core.Entities
{
    public class ShoutAsset
    {
        public int Id { get; set; }

        public int ShoutId { get; set; }

        public short AssetTypeId { get; set; }

        public string URL { get; set; }
    }
}
