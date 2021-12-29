using CSB.Core.Entities.Authentication;
using CSB.Core.Entities.Responses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CSB.Core.Web.Attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class CSBAuthorizeAttribute : Attribute, IAuthorizationFilter
    {
        public CSBAuthorizeAttribute()
        {

        }

        public string Roles { get; set; }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var userClaims = (UserClaims)context.HttpContext.Items["User"];
            if (userClaims == null)
            {
                context.Result = new JsonResult(ServiceResponse.Fail("Unauthorized")) { StatusCode = StatusCodes.Status401Unauthorized };
                return;
            }
            if (userClaims.Claims.Any(x => GetRoles().Contains(x.Name)) == false)
            {
                context.Result = new JsonResult(ServiceResponse.Fail("Unauthorized")) { StatusCode = StatusCodes.Status401Unauthorized };
                return;
            }
        }
        public IList<string> GetRoles()
        {
            return Roles.Split(',', '|');
        }
    }
}