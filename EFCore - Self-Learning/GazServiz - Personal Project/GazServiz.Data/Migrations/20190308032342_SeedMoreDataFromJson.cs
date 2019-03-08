using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GazServiz.Data.Migrations
{
    public partial class SeedMoreDataFromJson : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "IsFixed",
                table: "Repairs",
                nullable: false,
                defaultValue: "Nope",
                oldClrType: typeof(string));

            migrationBuilder.InsertData(
                table: "Cars",
                columns: new[] { "id", "LicensePlate", "Millage", "Model", "OwnerId" },
                values: new object[,]
                {
                    { 1, "�� 1016 ��", 100000.2, "Fiat", 1 },
                    { 2, "A 7777 ��", 15000.0, "Aston Martin", 3 },
                    { 3, "�� 1515 ��", 150000.98999999999, "BMW", 1 }
                });

            migrationBuilder.InsertData(
                table: "Emoloyees",
                columns: new[] { "id", "Name", "Speciality" },
                values: new object[,]
                {
                    { 1, "Bai Ivan", "Windows" },
                    { 2, "Todor", "Engine" },
                    { 3, "The Master", "GearBox" }
                });

            migrationBuilder.InsertData(
                table: "Repairs",
                columns: new[] { "id", "Bill", "CarId", "DateIn", "DateOut", "EmployeeId", "ProblemDescription" },
                values: new object[] { 2, null, 1, new DateTime(2019, 3, 8, 5, 23, 38, 193, DateTimeKind.Local).AddTicks(3866), null, 1, "��������� ������� �� ��. ���������" });

            migrationBuilder.InsertData(
                table: "Repairs",
                columns: new[] { "id", "Bill", "CarId", "DateIn", "DateOut", "EmployeeId", "ProblemDescription" },
                values: new object[] { 3, null, 3, new DateTime(2019, 3, 8, 5, 23, 38, 193, DateTimeKind.Local).AddTicks(4066), null, 2, "��������� �� ������" });

            migrationBuilder.InsertData(
                table: "Repairs",
                columns: new[] { "id", "Bill", "CarId", "DateIn", "DateOut", "EmployeeId", "ProblemDescription" },
                values: new object[] { 1, null, 2, new DateTime(2019, 3, 8, 5, 23, 38, 183, DateTimeKind.Local).AddTicks(1829), null, 3, "����� �� ����� - ��������� �����!" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Repairs",
                keyColumn: "id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Repairs",
                keyColumn: "id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Repairs",
                keyColumn: "id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Cars",
                keyColumn: "id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Cars",
                keyColumn: "id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Cars",
                keyColumn: "id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Emoloyees",
                keyColumn: "id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Emoloyees",
                keyColumn: "id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Emoloyees",
                keyColumn: "id",
                keyValue: 3);

            migrationBuilder.AlterColumn<string>(
                name: "IsFixed",
                table: "Repairs",
                nullable: false,
                oldClrType: typeof(string),
                oldDefaultValue: "Nope");
        }
    }
}
