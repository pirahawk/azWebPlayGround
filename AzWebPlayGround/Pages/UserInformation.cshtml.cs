using AzWebPlayGround.Middleware;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AzWebPlayGround.Pages
{
    //[ServiceFilter(type: typeof(MyJwtAuthFilterAttribute))]
    //[AllowAnonymous]
    public class UserInformationModel : PageModel
    {
        public void OnGet()
        {
            var context = this.HttpContext.User;
        }
    }
}
