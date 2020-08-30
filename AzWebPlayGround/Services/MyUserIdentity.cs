using System.Security.Principal;

namespace AzWebPlayGround.Services
{
    public class MyUserIdentity: IIdentity
    {
        public MyUser MyUser { get; set; }
        public string? AuthenticationType => "Custom-JwtTokenAuth";
        public bool IsAuthenticated => MyUser != null;
        public string? Name => MyUser.UserName;
    }
}