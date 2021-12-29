using System;

namespace CSB.Core.Utilities.Messaging
{
    public abstract class Message //: DocumentDbEntity
    {//Id IEntity ya da DocumentDbEntity'den gelecek.
        public string To { get; set; }
        public string Content { get; set; }
        public DateTime SendDate { get; set; } = DateTime.Now;
        public DateTime? StartSendDate { get; set; }
        public DateTime? EndSendDate { get; set; }
    }
}