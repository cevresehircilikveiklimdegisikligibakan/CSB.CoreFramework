using System;

namespace CSB.Core.Application.Attributes
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = false)]
    public class LogAttribute : Attribute
    {
    }
}