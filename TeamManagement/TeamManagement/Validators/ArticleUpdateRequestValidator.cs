using FluentValidation;
using TeamManagement.Contracts.v1.Requests;

namespace TeamManagement.Validators
{
    public class ArticleUpdateRequestValidator : AbstractValidator<ArticleUpdateRequest>
    {
        public ArticleUpdateRequestValidator()
        {
            RuleFor(x => x.Content).NotEmpty().WithMessage("Content is required");
            RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required");
            RuleFor(x => x.Status).NotEmpty().WithMessage("Status is required");
            RuleFor(x => x.TagId).NotEmpty().WithMessage("TagId is required");
        }
    }
}
