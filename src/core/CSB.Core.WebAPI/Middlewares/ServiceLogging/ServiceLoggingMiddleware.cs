using CSB.Core.Utilities.Logging;
using CSB.Core.Web;
using CSB.Core.Web.Entities.ServiceLogging;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Threading.Tasks;

namespace CSB.Core.WebAPI.Middlewares.ServiceLogging
{
    internal sealed class ServiceLoggingMiddleware
    {
        private readonly RequestDelegate next;
        private readonly ILogService _logService;
        public ServiceLoggingMiddleware(RequestDelegate next, ILogService logService)
        {
            this.next = next;
            _logService = logService;
        }

        public async Task Invoke(HttpContext context)
        {
            string requestData = "";
            Stream originalBody = context.Response.Body;

            try
            {
                string body = await context.GetRequestBodyAsync();
                using (var memStream = new MemoryStream())
                {
                    context.Response.Body = memStream;

                    await next(context);

                    memStream.Position = 0;
                    string responseBody = new StreamReader(memStream).ReadToEnd();

                    memStream.Position = 0;
                    await memStream.CopyToAsync(originalBody);
                    await _logService.LogAsync(
                        LogSettings<ServiceLog>.Create(
                            "WebApiServiceLog",
                            ServiceLog.Create(
                                context.Request.Host.Value,
                                context.Connection.RemoteIpAddress.ToString(),
                                requestData,
                                responseBody),
                            "WebApiServiceLog"));
                }
            }
            finally
            {
                context.Response.Body = originalBody;
            }

        }
    }
}