using Microsoft.EntityFrameworkCore.Migrations;

namespace ProBet.Migrations
{
    public partial class plsplsplspls : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Won",
                table: "Ticket",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "bit");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "Won",
                table: "Ticket",
                type: "bit",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);
        }
    }
}
