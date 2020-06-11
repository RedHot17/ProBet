using Microsoft.EntityFrameworkCore.Migrations;

namespace ProBet.Migrations
{
    public partial class oooohg : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Won",
                table: "Ticket",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Won",
                table: "Ticket");
        }
    }
}
