using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AuthSystem.Migrations
{
    public partial class businesstype : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {


         

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "tbl_BusinessTypeModel",
                type: "int",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "tbl_BusinessTypeModel");

            migrationBuilder.AddColumn<DateTime>(
                name: "DateUpdated",
                table: "tbl_BusinessTypeModel",
                type: "datetime",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Location",
                table: "tbl_BusinessTypeModel",
                type: "varchar(MAX)",
                nullable: true);
        }
    }
}
