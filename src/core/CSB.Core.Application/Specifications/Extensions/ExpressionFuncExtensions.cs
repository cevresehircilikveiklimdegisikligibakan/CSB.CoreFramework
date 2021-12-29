using CSB.Core.Expressions;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace CSB.Core.Application.Specifications
{
    internal static class ExpressionFuncExtensions
    {
        internal static Expression<Func<T, bool>> And<T>(this Expression<Func<T, bool>> left, Expression<Func<T, bool>> right)
        {
            return left.Compose(right, Expression.AndAlso);
        }
        internal static Expression<Func<T, bool>> Or<T>(this Expression<Func<T, bool>> left, Expression<Func<T, bool>> right)
        {
            return left.Compose(right, Expression.OrElse);
        }

        private static Expression<T> Compose<T>(this Expression<T> left, Expression<T> right, Func<Expression, Expression, Expression> merge)
        {
            var map = left.Parameters.Select((l, i) => new { l, r = right.Parameters[i] }).ToDictionary(p => p.r, p => p.l);

            var rightBody = ParameterRebinder.ReplaceParameters(map, right.Body);

            return Expression.Lambda<T>(merge(left.Body, rightBody), left.Parameters);
        }
    }
}