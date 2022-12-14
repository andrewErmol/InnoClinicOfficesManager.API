using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OfficesManager.Database.Migrations
{
    public partial class DatabaseCreation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Offices",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhotoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RegistryPhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Offices", x => x.ID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Offices");
        }
    }
}
