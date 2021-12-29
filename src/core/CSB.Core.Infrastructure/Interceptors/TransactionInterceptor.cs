using Castle.DynamicProxy;
using CSB.Core.Application.Attributes;
using CSB.Core.Application.Infrastructure.Persistence;
using CSB.Core.Exceptions;
using CSB.Core.Extensions;
using CSB.Core.Infrastructure.Extensions;
using System;

namespace CSB.Core.Infrastructure.Interceptors
{
    internal class TransactionInterceptor : IInterceptor
    {
        private static int ProcessOrder = 3;
        private readonly IServiceProvider _serviceProvider;

        public TransactionInterceptor(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public void Intercept(IInvocation invocation)
        {
            if (invocation.ShouldIntercept<TransactionAttribute>())
            {
                var attribute = invocation.GetAttribute<TransactionAttribute>();
                if (attribute == null || attribute.DataContextType == null)
                {
                    throw new CoreException("TransactionAttribute tanımında DataContextType bulunmamaktadır.");
                }
                IDataContext context;
                try
                {
                    context = (IDataContext)_serviceProvider.GetService(attribute.DataContextType);
                }
                catch
                {
                    throw new CoreException("TransactionAttribute tanımında bulunan DataContextType türü IDataContext türünde değildir.");
                }
                bool isRollback = false;
                using (var transaction = context.BeginTransaction())
                {
                    try
                    {
                        invocation.Proceed();
                        if (invocation.ReturnValue.GetType().IsGeneric())
                        {
                            if (invocation.ReturnValue.GetType().GenericTypeArguments[0].IsResponse())
                            {
                                if (HasResponseError(invocation))
                                {
                                    transaction.Rollback();
                                    isRollback = true;
                                }
                                else
                                {
                                    var value = GetServiceResponse(invocation);
                                    if (value != null && value.IsSuccess == false)
                                    {
                                        transaction.Rollback();
                                        isRollback = true;
                                    }
                                }
                            }
                        }
                        if (!isRollback)
                        {
                            transaction.Commit();
                        }
                    }
                    catch
                    {
                        transaction.Rollback();
                    }
                    finally
                    {
                        transaction.Dispose();
                    }
                    return;
                }
            }
            else
            {
                invocation.Proceed();
                return;
            }
        }

        private protected dynamic GetResponseValue(IInvocation invocation)
        {
            return ((dynamic)invocation.ReturnValue).Result.Data;
        }
        private protected dynamic GetServiceResponse(IInvocation invocation)
        {
            return ((dynamic)invocation.ReturnValue).Result;
        }
        private protected bool HasResponseError(IInvocation invocation)
        {
            var exception = (Exception)((dynamic)invocation.ReturnValue).Exception;
            return exception != null;
        }
    }
}