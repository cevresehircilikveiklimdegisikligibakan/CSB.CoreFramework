using CSB.Core.Entities;
using System;
using System.Linq.Expressions;

namespace CSB.Core.Application.Specifications
{
    public class ByIdSpecification<TEntity> : Specification<TEntity> where TEntity : IEntity<int>
    {
        private readonly int _id;

        protected ByIdSpecification() { }
        protected ByIdSpecification(int id)
        {
            _id = id;
        }

        public static ByIdSpecification<TEntity> Create(int id)
        {
            return new ByIdSpecification<TEntity>(id);
        }

        public override Expression<Func<TEntity, bool>> ToExpression()
        {
            return x => x.Id == _id;
        }
    }
}