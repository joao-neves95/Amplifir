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
