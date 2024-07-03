﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using SlotGameServer.Persistence.Context;

#nullable disable

namespace SlotGameServer.Persistence.Migrations
{
    [DbContext(typeof(SlotGameServerDbContext))]
    partial class SlotGameServerDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.20")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("SlotGameServer.Domain.Entities.GameBetEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<decimal>("BetAmount")
                        .HasColumnType("numeric");

                    b.Property<int>("ChosenNumber")
                        .HasColumnType("integer");

                    b.Property<int>("CreateUserId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<bool>("IsWin")
                        .HasColumnType("boolean");

                    b.Property<int>("LastModifiedUserId")
                        .HasColumnType("integer");

                    b.Property<int>("ResultNumber")
                        .HasColumnType("integer");

                    b.Property<int>("SessionId")
                        .HasColumnType("integer");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.HasIndex("SessionId");

                    b.ToTable("GameBets", (string)null);
                });

            modelBuilder.Entity("SlotGameServer.Domain.Entities.GameSessionEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("CreateUserId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<int>("LastModifiedUserId")
                        .HasColumnType("integer");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.ToTable("GameSessions", (string)null);
                });

            modelBuilder.Entity("SlotGameServer.Domain.Entities.GameBetEntity", b =>
                {
                    b.HasOne("SlotGameServer.Domain.Entities.GameSessionEntity", "GameSession")
                        .WithMany("GameBets")
                        .HasForeignKey("SessionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("GameSession");
                });

            modelBuilder.Entity("SlotGameServer.Domain.Entities.GameSessionEntity", b =>
                {
                    b.Navigation("GameBets");
                });
#pragma warning restore 612, 618
        }
    }
}
