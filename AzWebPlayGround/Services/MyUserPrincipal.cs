using System.Security.Principal;

namespace AzWebPlayGround.Services
{
    public class MyUserPrincipal : GenericPrincipal
    {
        private readonly MyUserIdentity _userIdentity;

        public MyUserPrincipal(MyUserIdentity userIdentity, string[] roles) 
            : base(userIdentity, roles)
        {
            _userIdentity = userIdentity;
        }
    }
}