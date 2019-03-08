using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GazServiz.Data.Migrations
{
    public partial class EnumToString : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Speciality",
                table: "Emoloyees",
                nullable: false,
                defaultValue: "Windows",
                oldClrType: typeof(int),
                oldDefaultValue: 4);

            migrationBuilder.UpdateData(
                table: "Emoloyees",
                keyColumn: "EmployeeId",
                keyValue: 1,
                column: "Speciality",
                value: "GearBox");

            migrationBuilder.UpdateData(
                table: "Emoloyees",
                keyColumn: "EmployeeId",
                keyValue: 2,
                column: "Speciality",
                value: "Windows");

            migrationBuilder.UpdateData(
                table: "Emoloyees",
                keyColumn: "EmployeeId",
                keyValue: 3,
                column: "Speciality",
                value: "Brakes");

            migrationBuilder.UpdateData(
                table: "Emoloyees",
                keyColumn: "EmployeeId",
                keyValue: 4,
                column: "Speciality",
                value: "SafetyComponents");

            migrationBuilder.UpdateData(
                table: "Emoloyees",
                keyColumn: "EmployeeId",
                keyValue: 5,
                column: "Speciality",
                value: "Tyres");

            migrationBuilder.UpdateData(
                table: "Emoloyees",
                keyColumn: "EmployeeId",
                keyValue: 6,
                column: "Speciality",
                value: "Engine");

            migrationBuilder.UpdateData(
                table: "Repairs",
                keyColumn: "RepairId",
                keyValue: 1,
                column: "DateIn",
                value: new DateTime(2019, 3, 6, 20, 52, 9, 654, DateTimeKind.Local).AddTicks(4905));

            migrationBuilder.UpdateData(
                table: "Repairs",
                keyColumn: "RepairId",
                keyValue: 2,
                column: "DateIn",
                value: new DateTime(2019, 3, 6, 20, 52, 9, 667, DateTimeKind.Local).AddTicks(4187));

            migrationBuilder.UpdateData(
                table: "Repairs",
                keyColumn: "RepairId",
                keyValue: 3,
                column: "DateIn",
                value: new DateTime(2019, 3, 6, 20, 52, 9, 667, DateTimeKind.Local).AddTicks(4377));

            migrationBuilder.UpdateData(
                table: "Repairs",
                keyColumn: "RepairId",
                keyValue: 4,
                column: "DateIn",
                value: new DateTime(2019, 3, 6, 20, 52, 9, 667, DateTimeKind.Local).AddTicks(4418));

            migrationBuilder.UpdateData(
                table: "Repairs",
                keyColumn: "RepairId",
                keyValue: 5,
                column: "DateIn",
                value: new DateTime(2019, 3, 6, 20, 52, 9, 667, DateTimeKind.Local).AddTicks(4438));

            migrationBuilder.UpdateData(
                table: "Repairs",
                keyColumn: "RepairId",
                keyValue: 6,
                column: "DateIn",
                value: new DateTime(2019, 3, 6, 20, 52, 9, 667, DateTimeKind.Local).AddTicks(4777));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Speciality",
                table: "Emoloyees",
                nullable: false,
                defaultValue: 4,
                oldClrType: typeof(string),
                oldDefaultValue: "Windows");

            migrationBuilder.UpdateData(
                table: "Emoloyees",
                keyColumn: "EmployeeId",
                keyValue: 1,
                column: "Speciality",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Emoloyees",
                keyColumn: "EmployeeId",
                keyValue: 2,
                column: "Speciality",
                value: 4);

            migrationBuilder.UpdateData(
                table: "Emoloyees",
                keyColumn: "EmployeeId",
                keyValue: 3,
                column: "Speciality",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Emoloyees",
                keyColumn: "EmployeeId",
                keyValue: 4,
                column: "Speciality",
                value: 5);

            migrationBuilder.UpdateData(
                table: "Emoloyees",
                keyColumn: "EmployeeId",
                keyValue: 5,
                column: "Speciality",
                value: 3);

            migrationBuilder.UpdateData(
                table: "Emoloyees",
                keyColumn: "EmployeeId",
                keyValue: 6,
                column: "Speciality",
                value: 2);

            migrationBuilder.UpdateData(
                table: "Repairs",
                keyColumn: "RepairId",
                keyValue: 1,
                column: "DateIn",
                value: new DateTime(2019, 3, 6, 20, 47, 44, 581, DateTimeKind.Local).AddTicks(3874));

            migrationBuilder.UpdateData(
                table: "Repairs",
                keyColumn: "RepairId",
                keyValue: 2,
                column: "DateIn",
                value: new DateTime(2019, 3, 6, 20, 47, 44, 596, DateTimeKind.Local).AddTicks(9727));

            migrationBuilder.UpdateData(
                table: "Repairs",
                keyColumn: "RepairId",
                keyValue: 3,
                column: "DateIn",
                value: new DateTime(2019, 3, 6, 20, 47, 44, 596, DateTimeKind.Local).AddTicks(9912));

            migrationBuilder.UpdateData(
                table: "Repairs",
                keyColumn: "RepairId",
                keyValue: 4,
                column: "DateIn",
                value: new DateTime(2019, 3, 6, 20, 47, 44, 596, DateTimeKind.Local).AddTicks(9989));

            migrationBuilder.UpdateData(
                table: "Repairs",
                keyColumn: "RepairId",
                keyValue: 5,
                column: "DateIn",
                value: new DateTime(2019, 3, 6, 20, 47, 44, 597, DateTimeKind.Local).AddTicks(15));

            migrationBuilder.UpdateData(
                table: "Repairs",
                keyColumn: "RepairId",
                keyValue: 6,
                column: "DateIn",
                value: new DateTime(2019, 3, 6, 20, 47, 44, 597, DateTimeKind.Local).AddTicks(66));
        }
    }
}
