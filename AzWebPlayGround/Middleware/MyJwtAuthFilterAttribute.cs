using AzWebPlayGround.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace AzWebPlayGround.Middleware
{
    public class MyJwtAuthFilterAttribute : Attribute, IAuthorizationFilter
    {
        private readonly IUserService _userService;

        public MyJwtAuthFilterAttribute(IUserService userService)
        {
            _userService = userService;
        }


        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var allowsAnonymous = context.ActionDescriptor.EndpointMetadata.OfType<AllowAnonymousAttribute>().Any();
            if (allowsAnonymous)
            {
                return;
            }

            var isAuth = context.HttpContext.User?.Identity?.IsAuthenticated ?? false;

            if (!isAuth)
            {
                context.Result = new StatusCodeResult((int) HttpStatusCode.Unauthorized);
            }
        }
    }
}