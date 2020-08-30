using Microsoft.AspNetCore.Antiforgery;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AzWebPlayGround.Domain;

namespace AzWebPlayGround.Services
{
    public interface IUserService
    {
        Task<MyUser> Login(string userName);
        Task RemoveAuthToken();
        Task<MyUser> TryGetAuthenticatedUser();
        Task ReIssueJwtToken(MyUser authUser);
    }

    public class UserService : IUserService
    {
        private static SymmetricSecurityKey SigningKey => new SymmetricSecurityKey(Encoding.UTF8.GetBytes(NamingValues.SYMM_KEY));
        private readonly IHttpContextAccessor _httpContextAccessor;
        private string Issuer => _httpContextAccessor.HttpContext.Request.Host.ToUriComponent();

        public UserService(IHttpContextAccessor httpContextAccessor, IAntiforgery anitAntiforgeryService)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<MyUser> Login(string userName)
        {
            if (userName == null)
            {
                return await Task.FromResult(null as MyUser);
            }
            
            var user = new MyUser
            {
                Id = Guid.NewGuid(),
                UserName = userName
            };

            CreateAndSetJWTToken(user);
            return await Task.FromResult(user);
        }

        private void CreateAndSetJWTToken(MyUser user)
        {
            var expiryTime = (DateTimeOffset.UtcNow + NamingValues.tokenExpiryTime);
            var key = SigningKey;
            var tokeDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(ClaimTypes.PrimarySid, user.Id.ToString()),
                }),
                Expires = expiryTime.UtcDateTime,
                Issuer = Issuer,
                Audience = Issuer,
                SigningCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature)
            };
            var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            var jwtSecurityToken = jwtSecurityTokenHandler.CreateJwtSecurityToken(tokeDescriptor);
            var jwtTokenStr = jwtSecurityTokenHandler.WriteToken(jwtSecurityToken);
            SetJwtToken(jwtSecurityToken, jwtTokenStr);
        }

        public async Task ReIssueJwtToken(MyUser authUser)
        {
            CreateAndSetJWTToken(authUser);
            await Task.CompletedTask;
        }

        public async Task RemoveAuthToken()
        {
            await Task.CompletedTask;
            _httpContextAccessor.HttpContext.Response.Cookies.Delete(NamingValues.JWT_BEARER_COOKIE_NAME);
        }

        public async Task<MyUser> TryGetAuthenticatedUser()
        {
            var hasJwtToken = _httpContextAccessor.HttpContext.Request.Cookies.TryGetValue(NamingValues.JWT_BEARER_COOKIE_NAME,
                out var jwtTokenString);

            if (!hasJwtToken)
            {
                return await Task.FromResult(null as MyUser);
            }

            var tokenHandler = new JwtSecurityTokenHandler();

            try
            {
                ClaimsPrincipal claimsPrincipal = tokenHandler.ValidateToken(jwtTokenString, 
                    new TokenValidationParameters
                    {
                        IssuerSigningKey = SigningKey,
                        ValidIssuer = Issuer,
                        ValidAudience = Issuer,

                        ValidateIssuerSigningKey = true,
                        ValidateAudience = true,
                        ValidateLifetime = true
                    },
                    out SecurityToken jwtToken);

                var hasUserId = Guid.TryParse(claimsPrincipal.FindFirst(ClaimTypes.PrimarySid)?.Value , out var userId);
                var userName = claimsPrincipal.FindFirst(ClaimTypes.Name)?.Value;

                if (hasUserId && !string.IsNullOrWhiteSpace(userName))
                {
                    return new MyUser
                    {
                        Id = userId,
                        UserName = userName
                    };
                }
            }
            catch (Exception e)
            {
                // purposely nothing
            }

            return await Task.FromResult(null as MyUser);
        }

        private void SetJwtToken(JwtSecurityToken jwtSecurityToken, string jwtToken)
        {
            CookieOptions options = new CookieOptions
            {
                Expires = jwtSecurityToken.ValidTo,
                HttpOnly = true,
            };

            _httpContextAccessor.HttpContext.Response.Cookies.Append(NamingValues.JWT_BEARER_COOKIE_NAME, jwtToken, options);
        }
    }
}
