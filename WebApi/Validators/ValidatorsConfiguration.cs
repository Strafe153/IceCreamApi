using Core.Dtos;
using FluentValidation;

namespace WebApi.Validators;

public static class ValidatorsConfiguration
{
    public static void AddApplicationValidators(this IServiceCollection services)
    {
        services.AddScoped<IValidator<IceCreamCreateUpdateDto>, IceCreamCreateUpdateValidator>();
    }
}
