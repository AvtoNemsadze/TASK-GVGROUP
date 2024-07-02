using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SlotGameServer.Persistence.Context;

namespace SlotGameServer.Persistence
{
    public static class PersistanceServicesRegistration
    {
        public static IServiceCollection ConfigurePersistenceServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<SlotGameServerDbContext>((options) =>
            {
                options.UseNpgsql(configuration.GetConnectionString("SlotGameServerConnectionString"));
            });

            services.AddEntityFrameworkNpgsql();

            return services;
        }
    }
}
// sdjska