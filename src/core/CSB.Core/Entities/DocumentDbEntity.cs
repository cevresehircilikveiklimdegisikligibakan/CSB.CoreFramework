using System;

namespace CSB.Core.Entities
{
    public abstract class DocumentDbEntity : EntityBase<string>
    {
        public DocumentDbEntity()
        {
            Id = Guid.NewGuid().ToString();
        }
    }
}