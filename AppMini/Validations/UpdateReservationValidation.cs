using AppMini.Dtos;
using FluentValidation;

namespace AppMini.Validations;

public class UpdateReservationValidator : AbstractValidator<UpdateReservationRequest>
{
    public UpdateReservationValidator()
    {
        RuleFor(x => x.GuestCount)
            .GreaterThanOrEqualTo(1)
            .WithMessage("Must be more than 1");

        RuleFor(x => x.ReservationDate)
            .Must(date => date >= DateTime.Now)
            .WithMessage("Date cannot be in the past");
    }
}
