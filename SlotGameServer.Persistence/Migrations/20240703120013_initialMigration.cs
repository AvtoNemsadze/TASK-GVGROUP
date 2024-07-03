using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace SlotGameServer.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class initialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "GameSessions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    CreateUserId = table.Column<int>(type: "integer", nullable: false),
                    LastModifiedUserId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameSessions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GameBets",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    SessionId = table.Column<int>(type: "integer", nullable: false),
                    BetAmount = table.Column<decimal>(type: "numeric", nullable: false),
                    ChosenNumber = table.Column<int>(type: "integer", nullable: false),
                    ResultNumber = table.Column<int>(type: "integer", nullable: false),
                    IsWin = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    CreateUserId = table.Column<int>(type: "integer", nullable: false),
                    LastModifiedUserId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameBets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GameBets_GameSessions_SessionId",
                        column: x => x.SessionId,
                        principalTable: "GameSessions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GameBets_SessionId",
                table: "GameBets",
                column: "SessionId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GameBets");

            migrationBuilder.DropTable(
                name: "GameSessions");
        }
    }
}
