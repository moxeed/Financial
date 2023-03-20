using Finanacial.Infrastructure.ZarinPal;
using Financial.ZarinPal.Dependencies;
using Microsoft.Extensions.DependencyInjection;

namespace Finanacial.Infrastructure
{
    public static class ServiceRegistartion
    {
        public static void AddInfrastructure(this IServiceCollection services) {
            services.AddScoped<IZarinPalRepository, ZarinPalRepository>();
        }
    }
}
