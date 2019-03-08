using Microsoft.EntityFrameworkCore.Migrations;

namespace GazServiz.Data.Migrations
{
    public partial class SeedOwnersFromJson : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Owners",
                keyColumn: "ownerId",
                keyValue: 200);

            migrationBuilder.DeleteData(
                table: "Owners",
                keyColumn: "ownerId",
                keyValue: 201);

            migrationBuilder.DeleteData(
                table: "Owners",
                keyColumn: "ownerId",
                keyValue: 202);

            migrationBuilder.AlterColumn<string>(
                name: "IsFixed",
                table: "Repairs",
                nullable: false,
                defaultValue: "Nope",
                oldClrType: typeof(string));

            migrationBuilder.InsertData(
                table: "Owners",
                columns: new[] { "ownerId", "FirstName", "LastName", "MiddleName", "Mobile" },
                values: new object[] { 1, "Sara", "Strom", null, "+01999345890" });

            migrationBuilder.InsertData(
                table: "Owners",
                columns: new[] { "ownerId", "FirstName", "LastName", "MiddleName", "Mobile" },
                values: new object[] { 2, "Panaiot", "Minkov", null, "0887200159" });

            migrationBuilder.InsertData(
                table: "Owners",
                columns: new[] { "ownerId", "FirstName", "LastName", "MiddleName", "Mobile" },
                values: new object[] { 3, "Vanq", "Kamburova", null, "0292393291" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Owners",
                keyColumn: "ownerId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Owners",
                keyColumn: "ownerId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Owners",
                keyColumn: "ownerId",
                keyValue: 3);

            migrationBuilder.AlterColumn<string>(
                name: "IsFixed",
                table: "Repairs",
                nullable: false,
                oldClrType: typeof(string),
                oldDefaultValue: "Nope");

            migrationBuilder.InsertData(
                table: "Owners",
                columns: new[] { "ownerId", "FirstName", "LastName", "MiddleName", "Mobile" },
                values: new object[] { 200, "Strahil", "Voivoda", null, "+01999345890" });

            migrationBuilder.InsertData(
                table: "Owners",
                columns: new[] { "ownerId", "FirstName", "LastName", "MiddleName", "Mobile" },
                values: new object[] { 201, "Valko", "Valkov", null, "0887200159" });

            migrationBuilder.InsertData(
                table: "Owners",
                columns: new[] { "ownerId", "FirstName", "LastName", "MiddleName", "Mobile" },
                values: new object[] { 202, "Bashibozuk", "Patriotov", null, "1231433423432" });
        }
    }
}
