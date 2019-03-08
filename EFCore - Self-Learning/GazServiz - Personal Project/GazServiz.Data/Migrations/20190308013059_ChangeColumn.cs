using Microsoft.EntityFrameworkCore.Migrations;

namespace GazServiz.Data.Migrations
{
    public partial class ChangeColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "OwnerId",
                table: "Owners");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Owners",
                newName: "ownerId");

            migrationBuilder.AlterColumn<string>(
                name: "IsFixed",
                table: "Repairs",
                nullable: false,
                defaultValue: "Nope",
                oldClrType: typeof(string));

            migrationBuilder.AddPrimaryKey(
                name: "OWNERID",
                table: "Owners",
                column: "ownerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "OWNERID",
                table: "Owners");

            migrationBuilder.RenameColumn(
                name: "ownerId",
                table: "Owners",
                newName: "id");

            migrationBuilder.AlterColumn<string>(
                name: "IsFixed",
                table: "Repairs",
                nullable: false,
                oldClrType: typeof(string),
                oldDefaultValue: "Nope");

            migrationBuilder.AddPrimaryKey(
                name: "OwnerId",
                table: "Owners",
                column: "id");
        }
    }
}
