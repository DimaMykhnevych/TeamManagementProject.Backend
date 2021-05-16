
using FluentValidation;
using TeamManagement.Contracts.v1.Requests;

namespace TeamManagement.Validators
{
    public class CreatePollRequestValidator : AbstractValidator<CreatePollRequest>
    {
        public CreatePollRequestValidator()
        {
            RuleFor(poll => poll.Name).NotEmpty().WithMessage("Poll name shouldn't be empty");
            RuleFor(poll => poll.Options).NotEmpty().WithMessage("You should add an option here");
            RuleForEach(poll => poll.Options).ChildRules(options =>
            {
                options.RuleFor(option => option.Name).NotEmpty().WithMessage("Option name shouldn't be null");
            });
        }
    }
}
