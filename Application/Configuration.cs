using Application.Services;
using Core.Interfaces.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Application
{
    public static class Configuration
    {
        public static void AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IIceCreamService, IceCreamService>();
        }
    }
}
