using AuthAPI.Commands;
using AuthAPI.Managers;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace AuthAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class AccountController : ControllerBase
    {
        private readonly IAccountManager _manager;
        public AccountController(IAccountManager manager)
        {
            _manager = manager;
        }

        [HttpPost]
        public async Task<IActionResult> SignUp([FromBody]SignUpCommand command)
        {
            var result = await _manager.SignUp(command);
            return StatusCode(201, Result.Succeed(result, HttpStatusCode.Created)); 
        }

        [HttpPost]
        public async Task<IActionResult> SignIn([FromBody]SignInCommand command)
        {
            var result = await _manager.SignIn(command);
            return Ok(Result.Succeed(result, HttpStatusCode.OK));
        }
    }
}
