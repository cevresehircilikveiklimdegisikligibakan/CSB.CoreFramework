using Microsoft.AspNetCore.Http;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace CSB.Core.Web
{
    public static class CSBCoreWebExtensions
    {
        public static async Task<string> GetRequestBodyAsync(this HttpContext context)
        {
            var body = "";
            if (context.Request.ContentLength != null || (context.Request.ContentLength > 0) ||
                context.Request.Body.CanSeek)
            {
                context.Request.EnableBuffering();
                context.Request.Body.Seek(0, SeekOrigin.Begin);
                using (var reader = new StreamReader(context.Request.Body, Encoding.Default, true, 1024, true))
                {
                    body = await reader.ReadToEndAsync();
                }
                context.Request.Body.Position = 0;
            }
            return body.ClearBody();
        }

        public static async Task<string> GetResponseBodyAsync(this HttpContext context, RequestDelegate next = null)
        {
            Stream originalBody = context.Response.Body;
            string body = "";
            using (var memStream = new MemoryStream())
            {
                context.Response.Body = memStream;

                if (next != null)
                    await next(context);

                memStream.Position = 0;
                body = new StreamReader(memStream).ReadToEnd();

                memStream.Position = 0;
                await memStream.CopyToAsync(originalBody);
            }
            return body.ClearBody();
        }

        public static string ClearBody(this string body)
        {
            return body.Replace("\r", "").Replace("\n", "");
        }

    }
}