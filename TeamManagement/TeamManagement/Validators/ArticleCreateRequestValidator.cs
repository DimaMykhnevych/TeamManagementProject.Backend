using FluentValidation;
using TeamManagement.Contracts.v1.Requests;

namespace TeamManagement.Validators
{
    public class ArticleCreateRequestValidator : AbstractValidator<ArticleCreateRequest>
    {
        public ArticleCreateRequestValidator()
        {
            RuleFor(x => x.Content).NotEmpty().WithMessage("Content is required");
            RuleFor(x => x.Name).NotEmpty().WithMessage("Name field is required");
            RuleFor(x => x.TagId).NotEmpty().WithMessage("TagId is required");
            RuleFor(x => x.Status).NotEmpty().WithMessage("Status is required");
        }
    }
}
