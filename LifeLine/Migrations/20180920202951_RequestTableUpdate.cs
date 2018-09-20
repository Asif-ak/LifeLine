using Microsoft.EntityFrameworkCore.Migrations;

namespace LifeLineWebAPi.Migrations
{
    public partial class RequestTableUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "RID",
                table: "Requests",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RID",
                table: "Requests");
        }
    }
}
