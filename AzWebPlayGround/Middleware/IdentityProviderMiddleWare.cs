using System.Linq;
using System.Threading.Tasks;
using AzWebPlayGround.Services;
using Microsoft.AspNetCore.Http;

namespace AzWebPlayGround.Middleware
{
    public class IdentityProviderMiddleWare
    {
        private readonly RequestDelegate _next;

        public IdentityProviderMiddleWare(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext, IUserService userService)
        {
            MyUser authUser = await userService.TryGetAuthenticatedUser();
            if (authUser != null)
            {
                httpContext.User = new MyUserPrincipal(new MyUserIdentity
                {
                    MyUser = authUser
                }, Enumerable.Empty<string>().ToArray());

                await userService.ReIssueJwtToken(authUser);
            }

            await _next(httpContext);
        }
    }
}