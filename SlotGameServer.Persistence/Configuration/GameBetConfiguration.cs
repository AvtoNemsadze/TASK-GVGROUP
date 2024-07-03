using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SlotGameServer.Domain;
using SlotGameServer.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlotGameServer.Persistence.Configuration
{
    public class GameBetConfiguration : IEntityTypeConfiguration<GameBetEntity>
    {
        public void Configure(EntityTypeBuilder<GameBetEntity> builder)
        {
            builder.ToTable("GameBets");  

            builder.HasKey(bet => bet.Id); 

            builder.Property(bet => bet.Id)
                .IsRequired()  
                .ValueGeneratedOnAdd();  

            builder.Property(bet => bet.BetAmount)
                .IsRequired();

            builder.Property(bet => bet.ChosenNumber)
                .IsRequired();

            builder.Property(bet => bet.ResultNumber)
                .IsRequired();

            builder.Property(bet => bet.IsWin)
                .IsRequired();

            builder.Property(bet => bet.CreatedAt)
                .IsRequired();

            builder.HasOne(bet => bet.GameSession)  
                .WithMany(session => session.GameBets)
                .HasForeignKey(bet => bet.SessionId);
        }
    }
}
