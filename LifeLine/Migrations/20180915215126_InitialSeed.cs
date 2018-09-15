using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LifeLineWebAPi.Migrations
{
    public partial class InitialSeed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Donors",
                columns: table => new
                {
                    DonorID = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    DonorName = table.Column<string>(maxLength: 50, nullable: false),
                    DonorBloodtype = table.Column<byte>(nullable: false),
                    DonorCellNumber = table.Column<string>(maxLength: 12, nullable: false),
                    City = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Donors", x => x.DonorID);
                });

            migrationBuilder.CreateTable(
                name: "Requestor",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    RequestorName = table.Column<string>(maxLength: 50, nullable: false),
                    RequestorCellNumber = table.Column<string>(maxLength: 12, nullable: false),
                    RequestedOn = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Requestor", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Requests",
                columns: table => new
                {
                    RequestID = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    RequestedBloodtype = table.Column<byte>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    RequestorID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Requests", x => x.RequestID);
                    table.ForeignKey(
                        name: "FK_Requests_Requestor_RequestorID",
                        column: x => x.RequestorID,
                        principalTable: "Requestor",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Requests_RequestorID",
                table: "Requests",
                column: "RequestorID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Donors");

            migrationBuilder.DropTable(
                name: "Requests");

            migrationBuilder.DropTable(
                name: "Requestor");
        }
    }
}
