using CSB.Core.Application.Services;
using CSB.Core.Application.Specifications;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace CSB.Core.Infrastructure.Specifications
{
    public sealed class ApplicationServiceTypesSpecification : Specification<Type>
    {
        private ApplicationServiceTypesSpecification() { }

        public static ApplicationServiceTypesSpecification Create()
        {
            return new ApplicationServiceTypesSpecification();
        }

        public override Expression<Func<Type, bool>> ToExpression()
        {
            return t => !t.IsInterface && !t.IsAbstract && t.GetInterfaces().Any(i => i.GetInterfaces().Any(ii => ii.Name == typeof(IApplicationService).Name));
        }
    }
}