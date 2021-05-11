using FluentValidation;
using TeamManagement.Contracts.v1.Requests;

namespace TeamManagement.Validators
{
    public class TagCreateRequestValidator : AbstractValidator<TagCreateRequest>
    {
        public TagCreateRequestValidator()
        {
            RuleFor(tag => tag.Label).NotEmpty().WithMessage("Tag label shouldn't be empty");
        }
    }
}
