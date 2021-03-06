﻿using System.Threading.Tasks;
using AzWebPlayGround.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace AzWebPlayGround.Pages
{
    [AllowAnonymous]
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly IUserService _userService;
        private readonly IAntiForgeryService _antiForgeryService;

       
        public IndexModel(ILogger<IndexModel> logger, IUserService userService, IAntiForgeryService antiForgeryService)
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
    }
}
