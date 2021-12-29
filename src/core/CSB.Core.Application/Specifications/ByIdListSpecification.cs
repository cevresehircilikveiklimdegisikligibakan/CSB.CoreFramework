using CSB.Core.Entities;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace CSB.Core.Application.Specifications
{
    public class ByIdListSpecification<TEntity> : Specification<TEntity> where TEntity : Entity
    {
        private readonly int[] _ids;

        private ByIdListSpecification() { }
        private ByIdListSpecification(int[] ids)
        {
            _ids = ids;
        }

        public static ByIdListSpecification<TEntity> Create(params int[] ids)
        {
            return new ByIdListSpecification<TEntity>(ids);
        }

        public override Expression<Func<TEntity, bool>> ToExpression()
        {
            return x => _ids.Contains(x.Id);
        }
    }
}