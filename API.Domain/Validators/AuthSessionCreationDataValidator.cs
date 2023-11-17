using API.Domain.Dto;
using FluentValidation;

namespace API.Domain.Validators;

public class AuthSessionCreationDataValidator : AbstractValidator<AuthSessionCreationDataDto>
{
    public AuthSessionCreationDataValidator()
    {
        RuleFor(x => x.Email)
            .NotNull()
            .NotEmpty()
            .MaximumLength(256)
            .EmailAddress();
        RuleFor(x => x.Password)
            .NotNull()
            .NotEmpty()
            .MinimumLength(8)
            .MaximumLength(128);
        RuleFor(x => x.TwoFactorCode);
        RuleFor(x => x.TwoFactorRecoveryCode);
    }
}