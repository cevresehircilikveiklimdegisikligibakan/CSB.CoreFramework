using System;
using System.Linq.Expressions;

namespace CSB.Core.Application.Specifications
{
    public interface ISpecification<T>
    {
        bool IsSatisfiedBy(T obj);
        Expression<Func<T, bool>> ToExpression();
    }
}