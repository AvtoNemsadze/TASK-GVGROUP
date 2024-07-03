using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SlotGameServer.Application.Contracts.Persistence;
using SlotGameServer.Persistence.Context;
using SlotGameServer.Persistence.Repository;

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

            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IGameBetRepository, GameBetRepository>();
            services.AddScoped<IGameSessionRepository, GameSessionRepository>();

            return services;
        }
    }
}