using FluentValidation;
using System.ComponentModel.DataAnnotations;

namespace AuthAPI.Commands
{
    public class SignInCommand
    {
        public string? Email { get; set; }
        public string? Password { get; set; }
    }

    public class SignInCommandValidator : AbstractValidator<SignInCommand>
    {
        public SignInCommandValidator()
        {
            RuleFor(arg => arg.Email)
                .Must(p => !string.IsNullOrWhiteSpace(p))
                .WithMessage("'{PropertyName}' is required field.");

            RuleFor(arg => arg.Password)
                .Must(p => !string.IsNullOrWhiteSpace(p))
                .WithMessage("'{PropertyName}' is required field.");
        }
    }
}
