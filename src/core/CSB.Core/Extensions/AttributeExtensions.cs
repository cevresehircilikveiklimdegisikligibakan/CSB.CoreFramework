using System.Linq;
using System.Reflection;

namespace CSB.Core.Extensions
{
    public static class AttributeExtensions
    {
        public static T GetCustomAttributeOrNull<T>(this MethodInfo methodInfo) where T : class
        {
            var attributes = methodInfo.GetCustomAttributes(true).OfType<T>().ToArray();
            if (attributes.Length > 0)
                return attributes[0];

            attributes = methodInfo.DeclaringType.GetTypeInfo().GetCustomAttributes(true).OfType<T>().ToArray();
            if (attributes.Length > 0)
                return attributes[0];

            return default(T);
        }
    }
}