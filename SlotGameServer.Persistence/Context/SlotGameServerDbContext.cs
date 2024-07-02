using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using SlotGameServer.Domain.Entities;

namespace SlotGameServer.Persistence.Context
{
    public class SlotGameServerDbContext : DbContext
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public SlotGameServerDbContext(DbContextOptions<SlotGameServerDbContext> options)
        : base(options)
        {
            try
            {
                _httpContextAccessor = this.GetInfrastructure().GetRequiredService<IHttpContextAccessor>();
            }
            catch (Exception)
            {
                _httpContextAccessor = new HttpContextAccessor();
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(SlotGameServerDbContext).Assembly);
        }

        public DbSet<ProductEntity> Products{ get; set; } 

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            return base.SaveChangesAsync(cancellationToken);
        }
    }
}
