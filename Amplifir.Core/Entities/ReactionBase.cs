/*
 * Copyright (c) 2019 - 2020 João Pedro Martins Neves (SHIVAYL) - All Rights Reserved.
 *
 * Amplifir and all its content is licensed under the GNU Lesser General Public License (LGPL),
 * version 3, located in the root of this project, under the name "LICENSE.md".
 *
 */

using Amplifir.Core.Interfaces;

namespace Amplifir.Core.Entities
{
    public abstract class ReactionBase : IReaction
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public short ReactionTypeId { get; set; }
    }
}
