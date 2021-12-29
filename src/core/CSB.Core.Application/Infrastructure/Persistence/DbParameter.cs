namespace CSB.Core.Application.Infrastructure.Persistence
{
    public sealed class DbParameter
    {
        public string Name { get; private set; }
        public object Value { get; private set; }

        public DbParameter(string name, object value)
        {
            Name = name;
            Value = value;
        }
    }
}