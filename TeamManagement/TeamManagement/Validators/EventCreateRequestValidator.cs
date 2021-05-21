using FluentValidation;
using TeamManagement.Contracts.v1.Requests;

namespace TeamManagement.Validators
{
    public class EventCreateRequestValidator : AbstractValidator<EventCreateRequest>
    {
        public EventCreateRequestValidator()
        {
            RuleFor(req => req.Title).NotEmpty().WithMessage("Title of event can't be empty");
            RuleFor(req => req.Attendies).Must(r => r.Length >= 1).WithMessage("Attendies list must not be empty.");
            RuleFor(req => req.DateTime).NotEmpty().WithMessage("DateTime should not be empty");
        }
    }
}
