using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ProBet.Migrations
{
    public partial class VeryFirst : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Gambler",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(maxLength: 20, nullable: true),
                    LastName = table.Column<string>(maxLength: 30, nullable: true),
                    FullName = table.Column<string>(nullable: true),
                    Nationality = table.Column<string>(maxLength: 20, nullable: true),
                    Earnings = table.Column<int>(nullable: true),
                    ProfilePicture = table.Column<string>(nullable: true),
                    CoverPhoto = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Gambler", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Match",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HomeTeam = table.Column<string>(nullable: true),
                    AwayTeam = table.Column<string>(nullable: true),
                    HomeOdds = table.Column<float>(nullable: false),
                    DrawOdds = table.Column<float>(nullable: false),
                    AwayOdds = table.Column<float>(nullable: false),
                    StartTime = table.Column<DateTime>(nullable: false),
                    HomeGoals = table.Column<int>(nullable: true),
                    AwayGoals = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Match", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Ticket",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BetTime = table.Column<DateTime>(nullable: false),
                    BetMoney = table.Column<float>(nullable: false),
                    WinMoney = table.Column<float>(nullable: false),
                    Tip = table.Column<int>(nullable: false),
                    MatchId = table.Column<int>(nullable: false),
                    GamblerId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ticket", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Ticket_Gambler_GamblerId",
                        column: x => x.GamblerId,
                        principalTable: "Gambler",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Ticket_Match_MatchId",
                        column: x => x.MatchId,
                        principalTable: "Match",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Ticket_GamblerId",
                table: "Ticket",
                column: "GamblerId");

            migrationBuilder.CreateIndex(
                name: "IX_Ticket_MatchId",
                table: "Ticket",
                column: "MatchId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Ticket");

            migrationBuilder.DropTable(
                name: "Gambler");

            migrationBuilder.DropTable(
                name: "Match");
        }
    }
}
