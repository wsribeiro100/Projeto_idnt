
using IDNT.TravelRoutes.Services;
using Microsoft.Extensions.DependencyInjection;


namespace IDNT.TravelRoutes
{
    public static class TravelRouteModule
    {
        public static IServiceCollection AddTravelRouteModule(this IServiceCollection services)
        {
            services.AddScoped<ITravelRouteService, TravelRouteService>();            
            return services;
        }
    }
}
