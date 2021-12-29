namespace CSB.Core.Entities
{
    public abstract class EntityBase<TPrimaryKey> : IEntity<TPrimaryKey>
    {
        public TPrimaryKey Id { get; set; }
    }
    public abstract class EntityBase : IEntity
    {

    }
}