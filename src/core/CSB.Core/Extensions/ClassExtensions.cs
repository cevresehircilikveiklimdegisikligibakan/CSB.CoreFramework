using CSB.Core.Entities.Responses;
using System;
using System.Collections.Generic;

namespace CSB.Core.Extensions
{
    public static class ClassExtensions
    {
        public static bool IsResponse(this Type type)
        {
            return type.Name == typeof(ServiceResponse<>).Name;
        }
        public static bool IsGenericList(this Type type)
        {
            return type.Name == typeof(List<>).Name && type.IsGeneric();
        }
        public static bool IsGeneric(this Type type)
        {
            return type.GenericTypeArguments.Length > 0;
        }
    }
}