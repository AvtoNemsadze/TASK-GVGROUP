using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore;
using SlotGameServer.Application.Constants;
using SlotGameServer.Domain;

namespace SlotGameServer.Persistence.Context
{
    public abstract class AuditableDbContext : DbContext
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        protected AuditableDbContext(DbContextOptions options, IHttpContextAccessor httpContextAccessor)
            : base(options)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            AddTimestamps();
            return base.SaveChangesAsync(cancellationToken);
        }

        private void AddTimestamps()
        {
            var entities = ChangeTracker.Entries().Where(x => x.Entity is BaseEntity
            && (x.State == EntityState.Added || x.State == EntityState.Modified));

            var currentUserId = _httpContextAccessor?.HttpContext?.User.FindFirst(CustomClaimTypes.Uid)?.Value ?? string.Empty;
            int.TryParse(currentUserId, out int userId);

            foreach (var entity in entities)
            {
                var baseEntity = (BaseEntity)entity.Entity;
                if (entity.State == EntityState.Added)
                {
                    baseEntity.CreatedAt = DateTime.UtcNow;
                    baseEntity.CreateUserId = userId;
                }
                else
                {
                    baseEntity.UpdatedAt = DateTime.UtcNow;
                    baseEntity.LastModifiedUserId = userId;
                }
            }
        }
    }

}
