using AppMini.Dtos;
using FluentValidation;

namespace AppMini.Validations;

public class CreateRestaurantValidation : AbstractValidator<CreateRestaurantRequest>
{
    public CreateRestaurantValidation()
    {
        RuleFor(x => x.Name).NotEmpty().WithMessage("Cannot be null");
        RuleFor(x => x.City).NotEmpty().WithMessage("Cannot be null");
    }
}
