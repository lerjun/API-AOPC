using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AuthSystem.Migrations
{
    public partial class vendormodel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tbl_VendorModel",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VendorName = table.Column<string>(type: "varchar(MAX)", nullable: false),
                    BusinessId = table.Column<int>(type: "int", nullable: true),
                    Description = table.Column<string>(type: "varchar(MAX)", nullable: true),
                    Services = table.Column<string>(type: "varchar(MAX)", nullable: true),
                    WebsiteUrl = table.Column<string>(type: "varchar(MAX)", nullable: true),
                    FeatureImg = table.Column<string>(type: "varchar(MAX)", nullable: true),
                    Gallery = table.Column<string>(type: "varchar(MAX)", nullable: true),
                    Cno = table.Column<string>(type: "varchar(MAX)", nullable: true),
                    Email = table.Column<string>(type: "varchar(MAX)", nullable: true),
                    VideoUrl = table.Column<string>(type: "varchar(MAX)", nullable: true),
                    VrUrl = table.Column<string>(type: "varchar(MAX)", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_VendorModel", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tbl_VendorModel");
        }
    }
}
