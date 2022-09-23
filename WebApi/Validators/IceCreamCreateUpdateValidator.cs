using Core.Dtos;
using FluentValidation;

namespace WebApi.Validators;

public class IceCreamCreateUpdateValidator : AbstractValidator<IceCreamCreateUpdateDto>
{
    public IceCreamCreateUpdateValidator()
    {
        RuleFor(i => i.Flavour)
            .NotEmpty()
            .WithMessage("Flavour is required")
            .MinimumLength(2)
            .WithMessage("The flavour length must be at least 2")
            .MaximumLength(30)
            .WithMessage("The flavour length must not be greater than 30 characters");

        RuleFor(i => i.Color)
            .MinimumLength(2)
            .WithMessage("The flavour length must be at least 2")
            .MaximumLength(30)
            .WithMessage("The flavour length must not be more than 30 characters");

        RuleFor(i => i.Price)
            .NotEmpty()
            .WithMessage("Price is required")
            .GreaterThan(0)
            .WithMessage("The price must be greater than 0")
            .LessThan(101)
            .WithMessage("The price must not be greater than 100");

        RuleFor(i => i.WeightInGrams)
            .NotEmpty()
            .WithMessage("Weight is required")
            .GreaterThan(19)
            .WithMessage("The weight must be at least 20 grams")
            .LessThan(201)
            .WithMessage("The price must not be greater than 200 grams");
    }
}
