using System;
using System.Linq.Expressions;

namespace CSB.Core.Application.Specifications
{
    public abstract class Specification<T> : ISpecification<T>
    {
        private Func<T, bool> _compiledExpression;

        private Func<T, bool> CompiledExpression
        {
            get => _compiledExpression ?? (_compiledExpression = ToExpression().Compile());
        }


        public abstract Expression<Func<T, bool>> ToExpression();

        public bool IsSatisfiedBy(T obj)
        {
            return CompiledExpression(obj);
            //Func<TEntity, bool> predicate = Expression().Compile();
            //return predicate(entity);
        }

        public static implicit operator Expression<Func<T, bool>>(Specification<T> specification)
        {
            return specification.ToExpression();
        }
    }
}