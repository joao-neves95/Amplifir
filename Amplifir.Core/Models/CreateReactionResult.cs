/*
 * Copyright (c) 2019 - 2020 João Pedro Martins Neves (SHIVAYL) - All Rights Reserved.
 *
 * Amplifir and all its content is licensed under the GNU Lesser General Public License (LGPL),
 * version 3, located in the root of this project, under the name "LICENSE.md".
 *
 */

using Amplifir.Core.Enums;

namespace Amplifir.Core.Models
{
    public class CreateReactionResult<TReaction>
    {
        public CreateReactionState State { get; set; }

        public EntityType EntityType { get; set; }

        public int EntityId { get; set; }

        public TReaction Reaction { get; set; }
    }
}
