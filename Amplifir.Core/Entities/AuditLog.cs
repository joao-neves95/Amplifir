/*
 * Copyright (c) 2019 - 2020 João Pedro Martins Neves (SHIVAYL) - All Rights Reserved.
 *
 * Amplifir and all its content is licensed under the GNU Lesser General Public License (LGPL),
 * version 3, located in the root of this project, under the name "LICENSE.md".
 *
 */

using System;

namespace Amplifir.Core.Entities
{
    public class AuditLog
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public string IPv4 { get; set; }

        public short EventTypeId { get; set; }

        public DateTime CreateDate { get; set; }
    }
}
