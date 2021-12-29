using System;

namespace CSB.Core.Application.Attributes
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = false)]
    public class TransactionAttribute : Attribute
    {
        public TransactionAttribute(Type dataContextType)
        {
            DataContextType = dataContextType;
        }

        public Type DataContextType { get; private set; }
    }
}