using FluentValidation;

namespace AuthAPI.Commands
{
    public class SignUpCommand
    {
        public string? Email { get; set; }
        public string? Username { get; set; }
        public string? Password { get; set; }
    }

    public class SignUpCommandValidator : AbstractValidator<SignUpCommand>
    {
        public SignUpCommandValidator()
        {
            RuleFor(arg => arg.Email)
                .Must(p => !string.IsNullOrWhiteSpace(p))
                .WithMessage("'{PropertyName}' is required field.");

            RuleFor(arg => arg.Username)
                .Must(p => !string.IsNullOrWhiteSpace(p))
                .WithMessage("'{PropertyName}' is required field.");

            RuleFor(arg => arg.Password)
                .Must(p => !string.IsNullOrWhiteSpace(p))
                .WithMessage("'{PropertyName}' is required field.");
        }
    }
}
