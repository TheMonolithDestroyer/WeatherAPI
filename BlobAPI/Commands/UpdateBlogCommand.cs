using FluentValidation;

namespace BlobAPI.Commands
{
    public class UpdateBlogCommand
    {
        public Guid Id { get; set; }
        public string BlogHeader { get; set; } = null!;
        public string BlogContent { get; set; } = null!;
    }

    public class UpdateBlogCommandValidator : AbstractValidator<UpdateBlogCommand>
    {
        public UpdateBlogCommandValidator()
        {
            RuleFor(arg => arg.Id)
                .Must(p => p != Guid.Empty)
                .WithMessage("'{PropertyName}' is out of range.");

            RuleFor(arg => arg.BlogHeader)
                .Must(p => !string.IsNullOrWhiteSpace(p))
                .WithMessage("'{PropertyName}' is required field.");
        }
    }
}
