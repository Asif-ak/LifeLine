using Microsoft.EntityFrameworkCore.Migrations;

namespace LifeLineWebAPi.Migrations
{
    public partial class FourthDonorTableUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Donors",
                maxLength: 30,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Email",
                table: "Donors");
        }
    }
}
