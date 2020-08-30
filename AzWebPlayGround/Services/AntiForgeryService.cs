using System;
using System.Threading.Tasks;
using AzWebPlayGround.Domain;
using Microsoft.AspNetCore.Antiforgery;
using Microsoft.AspNetCore.Http;

namespace AzWebPlayGround.Services
{
    public interface IAntiForgeryService
    {
        Task ReIssueAntiForgeryTokens();
        Task ClearTokens();
    }

    public class AntiForgeryService : IAntiForgeryService
    {
        private readonly IAntiforgery _antiforgery;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AntiForgeryService(IAntiforgery antiforgery, IHttpContextAccessor httpContextAccessor)
        {
            _antiforgery = antiforgery;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task ReIssueAntiForgeryTokens()
        {
            await Task.CompletedTask;
            //var antiforgeryTokenSet = _antiforgery.GetTokens(_httpContextAccessor.HttpContext);
            var antiforgeryTokenSet = _antiforgery.GetAndStoreTokens(_httpContextAccessor.HttpContext);


            var publicToken = antiforgeryTokenSet.RequestToken;
            var privateToken = antiforgeryTokenSet.CookieToken;
            var cookieExpiryTime = DateTimeOffset.UtcNow + NamingValues.tokenExpiryTime;
            var publicOptions = new CookieOptions
            {
                Expires = cookieExpiryTime,
                HttpOnly = false
            };
            var privateOptions = new CookieOptions
            {
                Expires = cookieExpiryTime,
                HttpOnly = false
            };

            _httpContextAccessor.HttpContext.Response.Cookies.Append(NamingValues.XSRF_PUBLIC_KEY_COOKIE_NAME, publicToken, publicOptions);
            //_httpContextAccessor.HttpContext.Response.Cookies.Append(NamingValues.XSRF_PRIVATE_KEY_COOKIE_NAME, privateToken, privateOptions);
        }

        public async Task ClearTokens()
        {
            await Task.CompletedTask;
            _httpContextAccessor.HttpContext.Response.Cookies.Delete(NamingValues.XSRF_PUBLIC_KEY_COOKIE_NAME);
            //_httpContextAccessor.HttpContext.Response.Cookies.Delete(NamingValues.XSRF_PRIVATE_KEY_COOKIE_NAME);
        }
    }
}