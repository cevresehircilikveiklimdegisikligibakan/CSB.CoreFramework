using CSB.Core.Services;
using CSB.Core.Utilities.Logging;
using CSB.Core.Web;
using CSB.Core.Web.Entities.ExceptionLogging;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using System;
using System.Net;
using System.Threading.Tasks;

namespace CSB.Core.WebAPI.Middlewares.ExceptionLogging
{
    internal sealed class ExceptionLoggingMiddleware
    {
        private readonly RequestDelegate next;
        private readonly ILogService _logService;
        private readonly ISerializer _serializer;

        public ExceptionLoggingMiddleware(RequestDelegate next, ILogService logService, ISerializer serializer)
        {
            this.next = next;
            _logService = logService;
            _serializer = serializer;
        }

        public async Task Invoke(HttpContext context)
        {
            string requestData = "";

            try
            {
                requestData = await context.GetRequestBodyAsync();
                await next(context);
            }
            catch (ValidationException exc)
            {
                string exceptionId = await Log(context, requestData, exc);
                ValidationLogResponse response = ValidationLogResponse.Create(exceptionId, exc.Message);
                await SetResponse(context, response, HttpStatusCode.UnprocessableEntity);
            }
            catch (Exception exc)
            {
                string exceptionId = await Log(context, requestData, exc);
                ExceptionLogResponse response = ExceptionLogResponse.Create(exceptionId);
                await SetResponse(context, response, HttpStatusCode.InternalServerError);
            }
        }
        private async Task<string> Log(HttpContext context, string requestData, Exception exception)
        {
            string exceptionId = Guid.NewGuid().ToString();
            await _logService.LogAsync(
                        LogSettings<ExceptionLog>.Create(
                            "WebApiExceptionLog",
                            ExceptionLog.Create(
                                exceptionId,
                                context.Request.Host.Value,
                                context.Connection.RemoteIpAddress.ToString(),
                                requestData,
                                exception.ToString()),
                            "WebApiExceptionLog"));
            return exceptionId;
        }
        private async Task SetResponse<T>(HttpContext context, T response, HttpStatusCode httpStatusCode) where T : ExceptionLogResponseBase
        {
            context.Response.StatusCode = httpStatusCode.GetHashCode();
            await context.Response.WriteAsync(_serializer.Serialize(response).ClearBody());
        }
    }
}