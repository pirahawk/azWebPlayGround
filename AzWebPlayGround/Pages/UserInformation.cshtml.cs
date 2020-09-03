using AzWebPlayGround.Services;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AzWebPlayGround.Pages
{
    //[ServiceFilter(type: typeof(MyJwtAuthFilterAttribute))]
    //[AllowAnonymous]
    public class UserInformationModel : PageModel
    {
        public MyUserPrincipal MyUser => HttpContext.User as MyUserPrincipal;


        public void OnGet()
        {
            var context = this.HttpContext.User;
        }
    }
}
