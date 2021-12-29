using System.Reflection;
using System.Threading.Tasks;

namespace CSB.Core.Extensions
{
    public static class MethodExtensions
    {
        public static bool IsAsync(this MethodInfo method)
        {
            return (method.ReturnType == typeof(Task) || (method.ReturnType.IsGenericType && method.ReturnType.GetGenericTypeDefinition() == typeof(Task<>)));
        }
        public static void SetPropertyValue<T>(this T data, string propertyName, object value) where T : class
        {
            data.GetType().GetProperty(propertyName).SetValue(data, value);
        }
    }
}