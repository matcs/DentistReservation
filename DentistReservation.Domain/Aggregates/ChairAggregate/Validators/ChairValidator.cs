using FluentValidation;

namespace DentistReservation.Domain.Aggregates.ChairAggregate.Validators;

public class ChairValidator : AbstractValidator<Chair>
{
    public ChairValidator()
    {
        RuleFor(c => c.Id).NotEqual(new Guid());
        RuleFor(c => c.EndMinute).GreaterThanOrEqualTo(0);
        RuleFor(c => c.Number).GreaterThanOrEqualTo(0);
        RuleFor(c => c.StartHour).GreaterThanOrEqualTo(0);
        RuleFor(c => c.StartMinute).GreaterThanOrEqualTo(0);
        RuleFor(c => c.EndHour).GreaterThanOrEqualTo(0);
        RuleFor(c => c.EndMinute).GreaterThanOrEqualTo(0);
        RuleFor(c => c.AverageDuration).GreaterThanOrEqualTo(0);
        RuleFor(c => c.AverageSetupInMinutes).GreaterThanOrEqualTo(0);
    }
}