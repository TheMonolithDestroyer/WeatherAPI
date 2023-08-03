using AuthAPI.Commands;
using AuthAPI.Engine.Exceptions;
using AuthAPI.Persistence;
using AuthAPI.Services;
using Microsoft.AspNetCore.Identity;

namespace AuthAPI.Managers
{
    public interface IAccountManager
    {
        Task<string> SignUp(SignUpCommand command);
        Task<SignInCommandResult> SignIn(SignInCommand command);
    }

    public class AccountManager : IAccountManager
    {
        private readonly ILogger<AccountManager> _logger;
        private readonly ITokenService _tokenService;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly UserContext _userContext;

        public AccountManager(
            ILogger<AccountManager> logger,
            ITokenService tokenService,
            UserManager<IdentityUser> userManager,
            UserContext userContext)
        {
            _logger = logger;
            _tokenService = tokenService;
            _userManager = userManager;
            _userContext = userContext;
        }

        public async Task<string> SignUp(SignUpCommand command)
        {
            var validationResult = new SignUpCommandValidator().Validate(command);
            if (!validationResult.IsValid)
                throw new BadRequestException(validationResult.ToString(" "));

            var result = await _userManager.CreateAsync(
                new IdentityUser { UserName = command.Username, Email = command.Email }, 
                command.Password!);

            _logger.LogInformation($"Create a new user: { command.Email }");

            if (!result.Succeeded)
                throw new BadRequestException(string.Join(". ", result.Errors.Select(i => i.Description)) );

            command.Password = "";
            return command.Email!;
        }

        public async Task<SignInCommandResult> SignIn(SignInCommand command)
        {
            var validationResult = new SignInCommandValidator().Validate(command);
            if (!validationResult.IsValid)
                throw new BadRequestException(validationResult.ToString(" "));

            var user = await _userManager.FindByEmailAsync(command.Email!);
            if (user == null)
                throw new BadRequestException("Bad credentials.");

            var passwordValid = await _userManager.CheckPasswordAsync(user, command.Password!);
            if (!passwordValid)
                throw new BadRequestException("Bad credentials.");

            var userInDb = _userContext.Users.FirstOrDefault(u => u.Email == command.Email);
            if (userInDb == null)
                throw new UnauthorizedException();

            var accessToken = _tokenService.CreateToken(userInDb);

            await _userContext.SaveChangesAsync();

            return new SignInCommandResult 
            {
                Username = userInDb.UserName!,
                Email = userInDb.Email!,
                Token = accessToken,
            };
        }
    }
}
