using Microsoft.AspNetCore.Mvc;

namespace AuthAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class AccountController : ControllerBase
    {
        public AccountController()
        {

        }

        public async Task<IActionResult> SignUp()
        {
            return Ok(); 
        }

        public async Task<IActionResult> SignIn()
        {
            return Ok();
        }
    }
}
