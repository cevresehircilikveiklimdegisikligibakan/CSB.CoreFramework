namespace CSB.Core.Entities
{
    public interface IEntity<TPrimaryKey> : IEntity
    {
        public TPrimaryKey Id { get; set; }
    }
    public interface IEntity
    {

    }
}