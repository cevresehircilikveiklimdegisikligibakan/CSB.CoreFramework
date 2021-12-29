using System;

namespace CSB.Core.Entities.Logging
{
    public record AuditLog
    {
        public string EntityName { get; set; } // EntityName
        public string OldEntityValue { get; set; } // OldEntityValue
        public string NewEntityValue { get; set; } // NewEntityValue
        public DateTime? LogDate { get; set; }
        public byte Type { get; set; }
        public string FullUrl { get; set; }
    }
}