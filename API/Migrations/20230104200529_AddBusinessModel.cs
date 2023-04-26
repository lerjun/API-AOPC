using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AuthSystem.Migrations
{
    public partial class AddBusinessModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tbl_BusinessModel",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BusinessName = table.Column<string>(type: "varchar(255)", nullable: false),
                    TypeId = table.Column<int>(type: "int", nullable: true),
                    LocationId = table.Column<int>(type: "int", nullable: true),
                    Description = table.Column<string>(type: "varchar(MAX)", nullable: false),
                    Address = table.Column<string>(type: "varchar(MAX)", nullable: false),
                    Cno = table.Column<string>(type: "varchar(MAX)", nullable: false),
                    Email = table.Column<string>(type: "varchar(MAX)", nullable: false),
                    Url = table.Column<string>(type: "varchar(MAX)", nullable: false),
                    Services = table.Column<string>(type: "varchar(MAX)", nullable: false),
                    ImageUrl = table.Column<string>(type: "varchar(MAX)", nullable: false),
                    Gallery = table.Column<string>(type: "varchar(MAX)", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_BusinessModel", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tbl_BusinessModel");
        }
    }
}
