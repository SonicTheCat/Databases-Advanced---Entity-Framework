using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GazServiz.Data.Migrations
{
    public partial class ChangeId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Owners",
                keyColumn: "OwnerId",
                keyValue: 1);

            migrationBuilder.AlterColumn<string>(
                name: "IsFixed",
                table: "Repairs",
                nullable: false,
                defaultValue: "Nope",
                oldClrType: typeof(string));

            migrationBuilder.InsertData(
                table: "Owners",
                columns: new[] { "OwnerId", "FirstName", "LastName", "MiddleName", "Mobile" },
                values: new object[] { 101, "George", "Lukas", null, "089877321" });

            migrationBuilder.UpdateData(
                table: "Repairs",
                keyColumn: "RepairId",
                keyValue: 1,
                columns: new[] { "DateIn", "IsFixed" },
                values: new object[] { new DateTime(2019, 3, 6, 21, 23, 17, 691, DateTimeKind.Local).AddTicks(2417), "Nope" });

            migrationBuilder.UpdateData(
                table: "Repairs",
                keyColumn: "RepairId",
                keyValue: 2,
                columns: new[] { "DateIn", "IsFixed" },
                values: new object[] { new DateTime(2019, 3, 6, 21, 23, 17, 699, DateTimeKind.Local).AddTicks(5881), "Nope" });

            migrationBuilder.UpdateData(
                table: "Repairs",
                keyColumn: "RepairId",
                keyValue: 3,
                columns: new[] { "DateIn", "IsFixed" },
                values: new object[] { new DateTime(2019, 3, 6, 21, 23, 17, 699, DateTimeKind.Local).AddTicks(5943), "Nope" });

            migrationBuilder.UpdateData(
                table: "Repairs",
                keyColumn: "RepairId",
                keyValue: 4,
                columns: new[] { "DateIn", "IsFixed" },
                values: new object[] { new DateTime(2019, 3, 6, 21, 23, 17, 699, DateTimeKind.Local).AddTicks(5953), "Nope" });

            migrationBuilder.UpdateData(
                table: "Repairs",
                keyColumn: "RepairId",
                keyValue: 5,
                columns: new[] { "DateIn", "IsFixed" },
                values: new object[] { new DateTime(2019, 3, 6, 21, 23, 17, 699, DateTimeKind.Local).AddTicks(5963), "Nope" });

            migrationBuilder.UpdateData(
                table: "Repairs",
                keyColumn: "RepairId",
                keyValue: 6,
                columns: new[] { "DateIn", "IsFixed" },
                values: new object[] { new DateTime(2019, 3, 6, 21, 23, 17, 699, DateTimeKind.Local).AddTicks(5968), "Nope" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Owners",
                keyColumn: "OwnerId",
                keyValue: 101);

            migrationBuilder.AlterColumn<string>(
                name: "IsFixed",
                table: "Repairs",
                nullable: false,
                oldClrType: typeof(string),
                oldDefaultValue: "Nope");

            migrationBuilder.InsertData(
                table: "Owners",
                columns: new[] { "OwnerId", "FirstName", "LastName", "MiddleName", "Mobile" },
                values: new object[] { 1, "George", "Lukas", null, "089877321" });

            migrationBuilder.UpdateData(
                table: "Repairs",
                keyColumn: "RepairId",
                keyValue: 1,
                columns: new[] { "DateIn", "IsFixed" },
                values: new object[] { new DateTime(2019, 3, 6, 20, 59, 20, 187, DateTimeKind.Local).AddTicks(7364), null });

            migrationBuilder.UpdateData(
                table: "Repairs",
                keyColumn: "RepairId",
                keyValue: 2,
                columns: new[] { "DateIn", "IsFixed" },
                values: new object[] { new DateTime(2019, 3, 6, 20, 59, 20, 199, DateTimeKind.Local).AddTicks(4559), null });

            migrationBuilder.UpdateData(
                table: "Repairs",
                keyColumn: "RepairId",
                keyValue: 3,
                columns: new[] { "DateIn", "IsFixed" },
                values: new object[] { new DateTime(2019, 3, 6, 20, 59, 20, 199, DateTimeKind.Local).AddTicks(4693), null });

            migrationBuilder.UpdateData(
                table: "Repairs",
                keyColumn: "RepairId",
                keyValue: 4,
                columns: new[] { "DateIn", "IsFixed" },
                values: new object[] { new DateTime(2019, 3, 6, 20, 59, 20, 199, DateTimeKind.Local).AddTicks(4724), null });

            migrationBuilder.UpdateData(
                table: "Repairs",
                keyColumn: "RepairId",
                keyValue: 5,
                columns: new[] { "DateIn", "IsFixed" },
                values: new object[] { new DateTime(2019, 3, 6, 20, 59, 20, 199, DateTimeKind.Local).AddTicks(4744), null });

            migrationBuilder.UpdateData(
                table: "Repairs",
                keyColumn: "RepairId",
                keyValue: 6,
                columns: new[] { "DateIn", "IsFixed" },
                values: new object[] { new DateTime(2019, 3, 6, 20, 59, 20, 199, DateTimeKind.Local).AddTicks(4770), null });
        }
    }
}
