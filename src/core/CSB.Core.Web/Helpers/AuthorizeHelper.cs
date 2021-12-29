using CSB.Core.Entities.Authentication;
using CSB.Core.Entities.Responses;
using CSB.Core.Web.Attributes;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Linq;

namespace CSB.Core.Web.Helpers
{
    public static class AuthorizeHelper
    {
        public static ServiceResponse CheckUserRole(HttpContext context, IList<OperationClaim> operationClaims)
        {
            var endpoint = context.Features.Get<Microsoft.AspNetCore.Http.Features.IEndpointFeature>()?.Endpoint;
            var attribute = endpoint?.Metadata.GetMetadata<CSBAuthorizeAttribute>();
            if (attribute != null)
            {
                var roles = attribute.GetRoles();
                var userClaims = operationClaims.Select(c => c.Name).ToList();
                var isRoleExists = roles.Any(x => userClaims.Contains(x));
                if (!isRoleExists)
                {
                    return ServiceResponse.Fail("");
                }
            }
            return ServiceResponse.Success();
        }
    }
}