using CSB.Core.Utilities.Logging;
using System.Collections.Generic;

namespace CSB.Core.Infrastructure.Persistence.Logging
{
    internal class AuditLog : Log
    {
        public List<CSB.Core.Entities.Logging.AuditLog> AuditLogs { get; set; }

        public static AuditLog Create(List<CSB.Core.Entities.Logging.AuditLog> logs)
        {
            return new AuditLog
            {
                AuditLogs = logs
            };
        }
    }
}