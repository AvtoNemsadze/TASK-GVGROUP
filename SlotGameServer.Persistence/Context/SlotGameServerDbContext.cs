using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using SlotGameServer.Domain.Entities;


namespace SlotGameServer.Persistence.Context
{
    public class SlotGameServerDbContext : AuditableDbContext
    {
        public SlotGameServerDbContext(DbContextOptions<SlotGameServerDbContext> options, IHttpContextAccessor httpContextAccessor)
            : base(options, httpContextAccessor)
        {
        }

        public DbSet<GameBetEntity> GameBets { get; set; }
        public DbSet<GameSessionEntity> GameSessions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(SlotGameServerDbContext).Assembly);
        }
    }

}
