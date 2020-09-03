using AzWebPlayGround.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using AzWebPlayGround.Domain;

namespace AzWebPlayGround.Pages
{
    [AllowAnonymous]
    public class ManualLoginModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly IUserService _userService;
        private readonly IAntiForgeryService _antiForgeryService;

        [BindProperty()]
        public MyUserModel MyUser { get; set; }

        public ManualLoginModel(ILogger<IndexModel> logger, IUserService userService, IAntiForgeryService antiForgeryService)
        {
            _logger = logger;
            _userService = userService;
            _antiForgeryService = antiForgeryService;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            await _userService.RemoveAuthToken();
            await _antiForgeryService.ClearTokens();
            var context = this.HttpContext.User;
            //ModelState.Clear();
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
                //return await Task.FromResult(RedirectToPage("error"));
            }

            var newUser = await _userService.Login(MyUser.UserName);
            await _antiForgeryService.ReIssueAntiForgeryTokens();
            return RedirectToPage("userinformation");
        }
    }
}
