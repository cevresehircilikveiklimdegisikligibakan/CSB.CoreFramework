using Autofac;
using Autofac.Builder;
using Autofac.Extras.DynamicProxy;
using Autofac.Features.Scanning;
using Castle.DynamicProxy;
using CSB.Core.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace CSB.Core.Infrastructure.Extensions
{
    public static class InterceptorExtensions
    {
        public static bool ShouldIntercept<T>(this IInvocation invocation) where T : Attribute
        {
            var attribute = invocation.GetAttribute<T>();
            return attribute != null;
        }
        public static MethodInfo GetMethodInfo(this IInvocation invocation)
        {
            MethodInfo method;
            try { method = invocation.Method; }
            catch { method = invocation.GetConcreteMethod(); }
            return method;
        }
        public static T GetAttribute<T>(this IInvocation invocation) where T : Attribute
        {
            var method = GetMethodInfo(invocation);
            var attribute = method.GetCustomAttributeOrNull<T>();
            return attribute;
        }

        public static IRegistrationBuilder<object, ScanningActivatorData, DynamicRegistrationStyle> AddInterceptors(this IRegistrationBuilder<object, ScanningActivatorData, DynamicRegistrationStyle> builder, Assembly assembly)
        {
            builder.AddInterceptors(assembly.GetInterceptors());
            return builder;
        }
        public static IRegistrationBuilder<object, ScanningActivatorData, DynamicRegistrationStyle> AddInterceptors(this IRegistrationBuilder<object, ScanningActivatorData, DynamicRegistrationStyle> builder, params Type[] interceptors)
        {
            foreach (Type interceptor in interceptors)
            {
                builder.InterceptedBy(interceptor);
            }
            return builder;
        }
        public static void RegisterInterceptors(this ContainerBuilder builder, Assembly assembly)
        {
            builder.RegisterAssemblyTypes(assembly)
                    .Where(t =>
                                t.IsInterface == false &&
                                t.IsAbstract == false &&
                                typeof(IInterceptor).IsAssignableFrom(t))
                    .AsSelf();
        }

        private static Type[] GetInterceptors(this Assembly assembly)
        {
            Type parentType = typeof(IInterceptor);
            Type[] types = assembly.GetTypes();
            IEnumerable<Type> interfaces = types.Where(t => t.GetInterfaces().Contains(parentType));
            var interceptors = interfaces.OrderBy(i => GetFieldValue(i, "ProcessOrder")).ToArray();
            return interceptors;
        }

        private static object GetFieldValue(object source, string fieldName)
        {
            var field = ((TypeInfo)source).DeclaredFields.Where(f => f.Name == fieldName).FirstOrDefault();
            return field.GetValue(null);
        }
    }
}