using FluentValidation;

namespace BlobAPI.Commands
{
    public class CreateBlogCommand
    {
        public string BlogHeader { get; set; } = null!;
        public string BlogContent { get; set; } = null!;
    }

    public class CreateBlogCommandValidator : AbstractValidator<CreateBlogCommand>
    {
        public CreateBlogCommandValidator()
        {
            RuleFor(arg => arg.BlogHeader)
                .Must(p => !string.IsNullOrWhiteSpace(p))
                .WithMessage("'{PropertyName}' is required field.");
        }
    }
}
