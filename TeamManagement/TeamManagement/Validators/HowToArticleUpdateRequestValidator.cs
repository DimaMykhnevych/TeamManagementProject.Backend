using FluentValidation;
using TeamManagement.Contracts.v1.Requests;

namespace TeamManagement.Validators
{
    public class HowToArticleUpdateRequestValidator : AbstractValidator<HowToArticleUpdateRequest>
    {
        public HowToArticleUpdateRequestValidator()
        {
            RuleFor(x => x.Name).NotEmpty().MaximumLength(60);
            RuleFor(x => x.Problem).NotEmpty();
            RuleFor(x => x.Solution).NotEmpty();
        }
    }
}
