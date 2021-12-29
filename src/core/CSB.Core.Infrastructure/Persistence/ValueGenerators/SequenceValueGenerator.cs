using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.ValueGeneration;

namespace CSB.Core.Infrastructure.Persistence.ValueGenerators
{
    internal class SequenceValueGenerator : ValueGenerator<int>
    {
        private string _schema;
        private string _sequenceName;

        public SequenceValueGenerator(string schema, string sequenceName)
        {
            _schema = schema;
            _sequenceName = sequenceName;
        }

        public override bool GeneratesTemporaryValues => false;

        public override int Next(EntityEntry entry)
        {
            using (var command = entry.Context.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = $"SELECT {(string.IsNullOrWhiteSpace(_schema) ? "" : _schema + ".")}{_sequenceName}.NEXTVAL FROM DUAL";
                entry.Context.Database.OpenConnection();
                using (var reader = command.ExecuteReader())
                {
                    reader.Read();
                    return reader.GetInt32(0);
                }
            }
        }
    }
}