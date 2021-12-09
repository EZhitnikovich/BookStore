using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BookStore.Persistence.Migrations
{
    public partial class DeleteEmailAddAddressToOrder : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Email",
                table: "Orders",
                newName: "Address");

            migrationBuilder.AddColumn<DateTime>(
                name: "PublicationDate",
                table: "Books",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PublicationDate",
                table: "Books");

            migrationBuilder.RenameColumn(
                name: "Address",
                table: "Orders",
                newName: "Email");
        }
    }
}
