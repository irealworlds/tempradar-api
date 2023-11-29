using API.Domain.Dto;
using FluentValidation;

namespace API.Domain.Validators;

public class AuthSessionCreationDataValidator : AbstractValidator<AuthSessionCreationDataDto>
{
    public AuthSessionCreationDataValidator()
    {
        this.RuleFor(x => x.Email)
            .NotNull()
            .NotEmpty()
            .MaximumLength(256)
            .EmailAddress();
        this.RuleFor(x => x.Password)
            .NotNull()
            .NotEmpty()
            .MinimumLength(8)
            .MaximumLength(128);
        this.RuleFor(x => x.TwoFactorCode);
        this.RuleFor(x => x.TwoFactorRecoveryCode);
    }
}