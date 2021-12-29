using CSB.Core.Entities;
using CSB.Core.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Linq;

namespace CSB.Core.Infrastructure.Extensions
{
    internal static class DbSetExtensions
    {
        internal static string GetPrimaryKeyProperyName<TEntity>(this DbSet<TEntity> dbSet) where TEntity : class, IEntity
        {
            return dbSet.EntityType.FindPrimaryKey().Properties.First()?.Name;
        }
        internal static object GetPrimaryKeyValue<TEntity>(this EntityEntry<TEntity> entry, string primaryKeyPropertyName) where TEntity : class, IEntity
        {
            return entry.Members.ToList().Where(x => x.Metadata.PropertyInfo.Name == primaryKeyPropertyName).SingleOrDefault()?.CurrentValue;
        }
        internal static void SetEntryPrimaryKeyValue<TEntity>(this DbSet<TEntity> dbSet, EntityEntry<TEntity> entry) where TEntity : class, IEntity
        {
            string primaryKeyPropertyName = GetPrimaryKeyProperyName(dbSet);
            var primaryKeyValue = GetPrimaryKeyValue(entry, primaryKeyPropertyName);
            entry.Entity.SetPropertyValue(primaryKeyPropertyName, primaryKeyValue);
        }
    }
}