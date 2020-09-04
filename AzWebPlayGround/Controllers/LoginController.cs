using System.Threading.Tasks;
using AzWebPlayGround.Domain;
using AzWebPlayGround.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AzWebPlayGround.Controllers
{
    [ApiController]
    [Route("api/login")]
    public class LoginController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IAntiForgeryService _antiForgeryService;

        public LoginController(IUserService userService, IAntiForgeryService antiForgeryService)
        {
            _userService = userService;
            _antiForgeryService = antiForgeryService;
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody]MyUserModel user)
        {
            if (string.IsNullOrWhiteSpace(user?.UserName))
            {
                return BadRequest();
            }

            var userModel = await _userService.Login(user.UserName);
            await _antiForgeryService.ReIssueAntiForgeryTokens();

            return Ok(userModel);
        }
    }

    
}