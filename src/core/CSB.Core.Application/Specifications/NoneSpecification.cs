using System;
using System.Linq.Expressions;

namespace CSB.Core.Application.Specifications
{
    public sealed class NoneSpecification<T> : Specification<T>
    {
        public override Expression<Func<T, bool>> ToExpression()
        {
            return o => false;
        }
    }
}