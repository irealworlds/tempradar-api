using API.Domain.Dto;
using FluentValidation;

namespace API.Domain.Validators;

public class CreatePinnedCityValidator : AbstractValidator<CreatePinnedCityDto>
{
    public CreatePinnedCityValidator()
    {
        RuleFor(x => x.Name);
        RuleFor(x => x.Latitude)
            .NotNull()
            .NotEmpty()
            .GreaterThanOrEqualTo(-90)
            .LessThanOrEqualTo(90);
        RuleFor(x => x.Longitude)
            .NotNull()
            .NotEmpty()
            .GreaterThanOrEqualTo(-180)
            .LessThanOrEqualTo(180);
    }
}