using Core.Interfaces.Repositories;
using DataAccess.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace DataAccess
{
    public static class RepositoriesConfiguration
    {
        public static void AddApplicationRepositories(this IServiceCollection services)
        {
            services.AddScoped<IIceCreamRepository, IceCreamRepository>();
        }
    }
}
