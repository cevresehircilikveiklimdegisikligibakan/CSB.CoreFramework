using CSB.Core.Entities;
using System;
using System.Linq.Expressions;

namespace CSB.Core.Application.Specifications
{
    public class AllSpecification<TEntity> : Specification<TEntity> where TEntity : IEntity<int>
    {
        protected AllSpecification() { }

        public static AllSpecification<TEntity> Create()
        {
            return new AllSpecification<TEntity>();
        }

        public override Expression<Func<TEntity, bool>> ToExpression()
        {
            return x => true;
        }
    }
}