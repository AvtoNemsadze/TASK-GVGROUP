using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using SlotGameServer.Domain.Entities;


namespace SlotGameServer.Persistence.Configuration
{
    public class GameSessionConfiguration : IEntityTypeConfiguration<GameSessionEntity>
    {
        public void Configure(EntityTypeBuilder<GameSessionEntity> builder)
        {
            builder.ToTable("GameSessions");  

            builder.HasKey(session => session.Id);  

            builder.Property(session => session.Id)
                .IsRequired()
                .ValueGeneratedOnAdd();  

            builder.Property(session => session.CreateUserId)
                .IsRequired();

            builder.Property(session => session.CreatedAt)
                .IsRequired();

            builder.HasMany(session => session.GameBets) 
                .WithOne(bet => bet.GameSession)
                .HasForeignKey(bet => bet.SessionId);
        }
    }
}
