using CSB.Core.Infrastructure.Persistence.ValueGenerators;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CSB.Core.Infrastructure.Extensions
{
    public static class EfExtensions
    {
        public static PropertyBuilder<TProperty> SetSequence<TProperty>(this PropertyBuilder<TProperty> property, string sequenceName, string scheme = "")
        {
            return property.ValueGeneratedOnAdd()
                            .HasValueGenerator((_, __) => new SequenceValueGenerator(scheme, sequenceName));
        }
    }
}