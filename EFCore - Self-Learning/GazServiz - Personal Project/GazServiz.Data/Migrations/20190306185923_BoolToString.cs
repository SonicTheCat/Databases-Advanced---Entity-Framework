using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GazServiz.Data.Migrations
{
    public partial class BoolToString : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "IsFixed",
                table: "Repairs",
                nullable: false,
                defaultValue: "Nope",
                oldClrType: typeof(bool),
                oldDefaultValue: false);

            migrationBuilder.UpdateData(
                table: "Repairs",
                keyColumn: "RepairId",
                keyValue: 1,
                columns: new[] { "DateIn", "IsFixed" },
                values: new object[] { new DateTime(2019, 3, 6, 20, 59, 20, 187, DateTimeKind.Local).AddTicks(7364), "Nope" });

            migrationBuilder.UpdateData(
                table: "Repairs",
                keyColumn: "RepairId",
                keyValue: 2,
                columns: new[] { "DateIn", "IsFixed" },
                values: new object[] { new DateTime(2019, 3, 6, 20, 59, 20, 199, DateTimeKind.Local).AddTicks(4559), "Nope" });

            migrationBuilder.UpdateData(
                table: "Repairs",
                keyColumn: "RepairId",
                keyValue: 3,
                columns: new[] { "DateIn", "IsFixed" },
                values: new object[] { new DateTime(2019, 3, 6, 20, 59, 20, 199, DateTimeKind.Local).AddTicks(4693), "Nope" });

            migrationBuilder.UpdateData(
                table: "Repairs",
                keyColumn: "RepairId",
                keyValue: 4,
                columns: new[] { "DateIn", "IsFixed" },
                values: new object[] { new DateTime(2019, 3, 6, 20, 59, 20, 199, DateTimeKind.Local).AddTicks(4724), "Nope" });

            migrationBuilder.UpdateData(
                table: "Repairs",
                keyColumn: "RepairId",
                keyValue: 5,
                columns: new[] { "DateIn", "IsFixed" },
                values: new object[] { new DateTime(2019, 3, 6, 20, 59, 20, 199, DateTimeKind.Local).AddTicks(4744), "Nope" });

            migrationBuilder.UpdateData(
                table: "Repairs",
                keyColumn: "RepairId",
                keyValue: 6,
                columns: new[] { "DateIn", "IsFixed" },
                values: new object[] { new DateTime(2019, 3, 6, 20, 59, 20, 199, DateTimeKind.Local).AddTicks(4770), "Nope" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "IsFixed",
                table: "Repairs",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(string),
                oldDefaultValue: "Nope");

            migrationBuilder.UpdateData(
                table: "Repairs",
                keyColumn: "RepairId",
                keyValue: 1,
                columns: new[] { "DateIn", "IsFixed" },
                values: new object[] { new DateTime(2019, 3, 6, 20, 52, 9, 654, DateTimeKind.Local).AddTicks(4905), false });

            migrationBuilder.UpdateData(
                table: "Repairs",
                keyColumn: "RepairId",
                keyValue: 2,
                columns: new[] { "DateIn", "IsFixed" },
                values: new object[] { new DateTime(2019, 3, 6, 20, 52, 9, 667, DateTimeKind.Local).AddTicks(4187), false });

            migrationBuilder.UpdateData(
                table: "Repairs",
                keyColumn: "RepairId",
                keyValue: 3,
                columns: new[] { "DateIn", "IsFixed" },
                values: new object[] { new DateTime(2019, 3, 6, 20, 52, 9, 667, DateTimeKind.Local).AddTicks(4377), false });

            migrationBuilder.UpdateData(
                table: "Repairs",
                keyColumn: "RepairId",
                keyValue: 4,
                columns: new[] { "DateIn", "IsFixed" },
                values: new object[] { new DateTime(2019, 3, 6, 20, 52, 9, 667, DateTimeKind.Local).AddTicks(4418), false });

            migrationBuilder.UpdateData(
                table: "Repairs",
                keyColumn: "RepairId",
                keyValue: 5,
                columns: new[] { "DateIn", "IsFixed" },
                values: new object[] { new DateTime(2019, 3, 6, 20, 52, 9, 667, DateTimeKind.Local).AddTicks(4438), false });

            migrationBuilder.UpdateData(
                table: "Repairs",
                keyColumn: "RepairId",
                keyValue: 6,
                columns: new[] { "DateIn", "IsFixed" },
                values: new object[] { new DateTime(2019, 3, 6, 20, 52, 9, 667, DateTimeKind.Local).AddTicks(4777), false });
        }
    }
}
