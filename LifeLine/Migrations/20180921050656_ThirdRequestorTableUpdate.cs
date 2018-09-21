using Microsoft.EntityFrameworkCore.Migrations;

namespace LifeLineWebAPi.Migrations
{
    public partial class ThirdRequestorTableUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "City",
                table: "Requestor",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DonationAddress",
                table: "Requestor",
                maxLength: 75,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Requestor",
                maxLength: 30,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "City",
                table: "Requestor");

            migrationBuilder.DropColumn(
                name: "DonationAddress",
                table: "Requestor");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "Requestor");
        }
    }
}
