using API.Domain.Dto;
using FluentValidation;

namespace API.Domain.Validators;

public class IdentityCreationDataValidator : AbstractValidator<IdentityCreationDataDto>
{
    public IdentityCreationDataValidator()
    {
        this.RuleFor(x => x.FirstName)
            .NotNull()
            .NotEmpty()
            .MaximumLength(64);
        this.RuleFor(x => x.LastName)
            .NotNull()
            .NotEmpty()
            .MaximumLength(64);
        this.RuleFor(x => x.Password)
            .NotNull()
            .NotEmpty()
            .MinimumLength(8)
            .MaximumLength(128);
        this.RuleFor(x => x.Email)
            .NotNull()
            .NotEmpty()
            .MaximumLength(256)
            .EmailAddress();
    }
}