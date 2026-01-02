using AppMini.Dtos;
using FluentValidation;

namespace AppMini.Validations;

public class CreateDiningTableValidation : AbstractValidator<CreateDiningTableRequest>
{
    public CreateDiningTableValidation()
    {
        RuleFor(x => x.Capacity)
            .GreaterThanOrEqualTo(1)
            .WithMessage("Must be more than 1");

        RuleFor(x => x.DiningTableNumber)
            .GreaterThan(0)
            .WithMessage("Must be more than 0");
    }
}
