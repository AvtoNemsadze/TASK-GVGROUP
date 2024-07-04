using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SlotGameServer.Identity.Migrations
{
    /// <inheritdoc />
    public partial class addStatisticFieldsForUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TotalGamesPlayed",
                table: "Users",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TotalLosses",
                table: "Users",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TotalWins",
                table: "Users",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash", "TotalGamesPlayed", "TotalLosses", "TotalWins" },
                values: new object[] { "bf08d84d-0b01-43e6-82a0-d769d67214c0", new DateTime(2024, 7, 4, 13, 9, 37, 843, DateTimeKind.Utc).AddTicks(7694), "AQAAAAIAAYagAAAAEKARFcw2Nvzns4rbq09dbPhtYWto9xJsiiZX4AKEppZ+fDojWDrPycDiHxsz2ZMHiw==", 0, 0, 0 });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash", "TotalGamesPlayed", "TotalLosses", "TotalWins" },
                values: new object[] { "79064f52-4b2b-488f-8d3f-5cfb7c51d949", new DateTime(2024, 7, 4, 13, 9, 37, 917, DateTimeKind.Utc).AddTicks(3116), "AQAAAAIAAYagAAAAEPg6eMr7YBDQBXlDPdCCG3DT+6pm3fDQHXtHsMlj/JkA4drDwT4buBpnyGhoSCzwMg==", 0, 0, 0 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TotalGamesPlayed",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "TotalLosses",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "TotalWins",
                table: "Users");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash" },
                values: new object[] { "5555cef5-e120-4264-92f3-3f65348fd96a", new DateTime(2024, 7, 3, 19, 12, 45, 579, DateTimeKind.Utc).AddTicks(8), "AQAAAAIAAYagAAAAEMBIf6kyQOq+Ng9Kufou9rdZv2MxFzmCLznRRDyZKxW44KQ3yfA2fmNhm/3QqePvVw==" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash" },
                values: new object[] { "79bef5a4-1697-4d68-8a05-0d967157d5c7", new DateTime(2024, 7, 3, 19, 12, 45, 658, DateTimeKind.Utc).AddTicks(9593), "AQAAAAIAAYagAAAAEKdpe+C5+pft4LKbhA6Di59q8caVv4CrL2Vd1/mC6BBDGhN75/vJlNESpi35qwnGLA==" });
        }
    }
}
