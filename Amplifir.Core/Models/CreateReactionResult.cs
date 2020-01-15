/*
 * Copyright (c) 2019 - 2020 João Pedro Martins Neves (SHIVAYL) - All Rights Reserved.
 *
 * Amplifir and all its content is licensed under the GNU Lesser General Public License (LGPL),
 * version 3, located in the root of this project, under the name "LICENSE.md".
 *
 */

using Amplifir.Core.Enums;
using Amplifir.Core.Interfaces;

namespace Amplifir.Core.Models
{
    public class CreateReactionResult
    {
        public CreateReactionState State { get; set; }

        public EntityType EntityType { get; set; }

        /// <summary>
        ///
        /// The ID of the shout/comment this reaction belongs to.
        /// 
        /// </summary>
        public int EntityId { get; set; }

        public IReaction Reaction { get; set; }
    }
}
