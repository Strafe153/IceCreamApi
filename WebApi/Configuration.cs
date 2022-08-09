using Core.ViewModels;
using FluentValidation;
using WebApi.Validators;

namespace WebApi
{
    public static class Configuration
    {
        public static void AddApplicationValidators(this IServiceCollection services)
        {
            services.AddScoped<IValidator<IceCreamCreateUpdateViewModel>, IceCreamCreateUpdateValidator>();
        }

        public static void AddApplicationMiddleware(this IApplicationBuilder builder)
        {
            builder.UseMiddleware<ExceptionMiddleware>();
        }
    }
}
