using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using SlotGameServer.Domain.Entities;
using SlotGameServer.Domain;
using SlotGameServer.Application.Constants;

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
            AddTimestamps();
            return base.SaveChangesAsync(cancellationToken);
        }


        private void AddTimestamps()
        {
            var entities = ChangeTracker.Entries().Where(x => x.Entity is BaseEntity
            && (x.State == EntityState.Added || x.State == EntityState.Modified));

            var currentApplicationId = _httpContextAccessor?.HttpContext?.User.FindFirst(CustomClaimTypes.Uid)?.Value ?? string.Empty;

            foreach (var entity in entities)
            {
                if (entity.State == EntityState.Added || entity.State == EntityState.Modified)
                {
                    if (int.TryParse(currentApplicationId, out int userId))
                    {
                        if (entity.State == EntityState.Added)
                        {
                            ((BaseEntity)entity.Entity).CreatedAt = DateTime.Now;
                            ((BaseEntity)entity.Entity).CreateUserId = userId;
                        }
                        else
                        {
                            ((BaseEntity)entity.Entity).UpdatedAt = DateTime.Now;
                            ((BaseEntity)entity.Entity).LastModifiedUserId = userId;
                        }
                    }
                    else
                    {
                        if (entity.State == EntityState.Added)
                        {
                            ((BaseEntity)entity.Entity).CreatedAt = DateTime.Now;
                        }
                        else
                        {
                            ((BaseEntity)entity.Entity).UpdatedAt = DateTime.Now;
                        }
                    }
                }
            }
        }
    }
}
