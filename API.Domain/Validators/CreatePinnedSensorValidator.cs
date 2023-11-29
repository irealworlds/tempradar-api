using API.Domain.Dto;
using FluentValidation;

namespace API.Domain.Validators;

public class CreatePinnedSensorValidator : AbstractValidator<CreatePinnedSensorDto>
{
    public CreatePinnedSensorValidator()
    {
        this.RuleFor(x => x.Name);
        this.RuleFor(x => x.SensorId)
            .NotEmpty();
    }
}