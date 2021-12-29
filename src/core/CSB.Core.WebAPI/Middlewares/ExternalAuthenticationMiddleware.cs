using CSB.Core.Utilities.Security;
using CSB.Core.Web.Helpers;
using Microsoft.AspNetCore.Http;
using System.Linq;
using System.Threading.Tasks;

namespace CSB.Core.WebAPI.Middlewares
{
    internal sealed class ExternalAuthenticationMiddleware
    {
        private readonly RequestDelegate _next;

        private readonly ITokenService _tokenService;

        private string _authorizationHeader = "Authorization";

        public ExternalAuthenticationMiddleware(RequestDelegate next, ITokenService tokenService)
        {
            _next = next;

            _tokenService = tokenService;
        }

        public async Task Invoke(HttpContext context)
        {
            var token = context.Request.Headers[_authorizationHeader].FirstOrDefault()?.Split(" ").Last();
            if (token != null)
            {
                AttachUserToContext(context, token);
            }

            await _next(context);
        }

        private void AttachUserToContext(HttpContext context, string token)
        {
            var userClaimsResponse = _tokenService.GetUserClaims(token);
            if (userClaimsResponse.IsSuccess == false)
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                return;
            }
            var isRoleExist = AuthorizeHelper.CheckUserRole(context, userClaimsResponse.Data.Claims);
            if (isRoleExist.IsSuccess == false)
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                return;
            }
            context.Items["User"] = userClaimsResponse.Data;
        }
    }
}