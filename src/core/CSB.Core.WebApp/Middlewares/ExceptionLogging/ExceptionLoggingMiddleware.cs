using CSB.Core.Services;
using CSB.Core.Utilities.Logging;
using CSB.Core.Web;
using CSB.Core.Web.Entities.ExceptionLogging;
using Microsoft.AspNetCore.Http;
using System;
using System.Net;
using System.Threading.Tasks;

namespace CSB.Core.WebApp.Middlewares.ExceptionLogging
{
    internal sealed class ExceptionLoggingMiddleware
    {
        private readonly RequestDelegate next;

        public ExceptionLoggingMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task Invoke(HttpContext context, ILogService _logService, ISerializer _serializer)
        {
            string requestData = "";

            try
            {
                requestData = await context.GetRequestBodyAsync();
                await next(context);
            }
            catch (Exception exc)
            {
                string exceptionId = Guid.NewGuid().ToString();
                await _logService.LogAsync(
                            LogSettings<ExceptionLog>.Create(
                                "WebAppExceptionLog",
                                ExceptionLog.Create(
                                    exceptionId,
                                    context.Request.Host.Value,
                                    context.Connection.RemoteIpAddress.ToString(),
                                    requestData,
                                    exc.ToString()),
                                "WebAppExceptionLog")); ;

                context.Response.StatusCode = HttpStatusCode.InternalServerError.GetHashCode();
                ExceptionLogResponse response = ExceptionLogResponse.Create(exceptionId);
                await context.Response.WriteAsync(_serializer.Serialize(response).ClearBody());
            }
        }
    }
}