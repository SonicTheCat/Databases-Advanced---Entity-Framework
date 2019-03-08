using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GazServiz.Data.Migrations
{
    public partial class AddEncapsulation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //Firstly we need to drop all Foreign key constraints  
            migrationBuilder.DropForeignKey("FK_Cars_Owners_OwnerId", "Cars", null);
            migrationBuilder.DropForeignKey("FK_Repairs_Cars_CarId", "Repairs", null);
            migrationBuilder.DropForeignKey("FK_Repairs_Emoloyees_EmployeeId", "Repairs", null);

            migrationBuilder.DropPrimaryKey(
                name: "PK_Repairs",
                table: "Repairs");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Owners",
                table: "Owners");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Emoloyees",
                table: "Emoloyees");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Cars",
                table: "Cars");

            migrationBuilder.DeleteData(
                table: "Cars",
                keyColumn: "CarId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Cars",
                keyColumn: "CarId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Cars",
                keyColumn: "CarId",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Cars",
                keyColumn: "CarId",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Cars",
                keyColumn: "CarId",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Owners",
                keyColumn: "OwnerId",
                keyValue: 101);

            migrationBuilder.DeleteData(
                table: "Repairs",
                keyColumn: "RepairId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Repairs",
                keyColumn: "RepairId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Repairs",
                keyColumn: "RepairId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Repairs",
                keyColumn: "RepairId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Repairs",
                keyColumn: "RepairId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Repairs",
                keyColumn: "RepairId",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Cars",
                keyColumn: "CarId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Cars",
                keyColumn: "CarId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Cars",
                keyColumn: "CarId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Cars",
                keyColumn: "CarId",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Cars",
                keyColumn: "CarId",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Cars",
                keyColumn: "CarId",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Emoloyees",
                keyColumn: "EmployeeId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Emoloyees",
                keyColumn: "EmployeeId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Emoloyees",
                keyColumn: "EmployeeId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Emoloyees",
                keyColumn: "EmployeeId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Emoloyees",
                keyColumn: "EmployeeId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Emoloyees",
                keyColumn: "EmployeeId",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Owners",
                keyColumn: "OwnerId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Owners",
                keyColumn: "OwnerId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Owners",
                keyColumn: "OwnerId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Owners",
                keyColumn: "OwnerId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Owners",
                keyColumn: "OwnerId",
                keyValue: 6);

            migrationBuilder.RenameColumn(
                name: "RepairId",
                table: "Repairs",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "OwnerId",
                table: "Owners",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "EmployeeId",
                table: "Emoloyees",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "TotalMillage",
                table: "Cars",
                newName: "Millage");

            migrationBuilder.RenameColumn(
                name: "CarId",
                table: "Cars",
                newName: "id");

            migrationBuilder.AlterColumn<string>(
                name: "IsFixed",
                table: "Repairs",
                nullable: false,
                defaultValue: "Nope",
                oldClrType: typeof(string));

            migrationBuilder.AddPrimaryKey(
                name: "RepairId",
                table: "Repairs",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "OwnerId",
                table: "Owners",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "EmployeeId",
                table: "Emoloyees",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "CarId",
                table: "Cars",
                column: "id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "RepairId",
                table: "Repairs");

            migrationBuilder.DropPrimaryKey(
                name: "OwnerId",
                table: "Owners");

            migrationBuilder.DropPrimaryKey(
                name: "EmployeeId",
                table: "Emoloyees");

            migrationBuilder.DropPrimaryKey(
                name: "CarId",
                table: "Cars");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Repairs",
                newName: "RepairId");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Owners",
                newName: "OwnerId");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Emoloyees",
                newName: "EmployeeId");

            migrationBuilder.RenameColumn(
                name: "Millage",
                table: "Cars",
                newName: "TotalMillage");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Cars",
                newName: "CarId");

            migrationBuilder.AlterColumn<string>(
                name: "IsFixed",
                table: "Repairs",
                nullable: false,
                oldClrType: typeof(string),
                oldDefaultValue: "Nope");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Repairs",
                table: "Repairs",
                column: "RepairId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Owners",
                table: "Owners",
                column: "OwnerId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Emoloyees",
                table: "Emoloyees",
                column: "EmployeeId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Cars",
                table: "Cars",
                column: "CarId");

            migrationBuilder.InsertData(
                table: "Cars",
                columns: new[] { "CarId", "LicensePlate", "Model", "OwnerId", "TotalMillage" },
                values: new object[] { 2, "PP1010AA", "Honda Accord", 1, 100000.0 });

            migrationBuilder.InsertData(
                table: "Emoloyees",
                columns: new[] { "EmployeeId", "Name", "Speciality" },
                values: new object[,]
                {
                    { 1, "Bat Pesho", "GearBox" },
                    { 2, "Boiko Borisov", "Windows" },
                    { 3, "Moni Todorov", "Brakes" },
                    { 4, "Petar Petrov", "SafetyComponents" },
                    { 5, "Mishelin Georgiev", "Tyres" },
                    { 6, "Bai Ivan", "Engine" }
                });

            migrationBuilder.InsertData(
                table: "Owners",
                columns: new[] { "OwnerId", "FirstName", "LastName", "MiddleName", "Mobile" },
                values: new object[,]
                {
                    { 101, "George", "Lukas", null, "089877321" },
                    { 2, "Michael", "Carric", null, "004410201020" },
                    { 3, "Wayne", "Rooney", null, "004410102099" },
                    { 4, "Eric", "Cantona", null, "+3590828219" },
                    { 5, "Benito", "Musolini", null, "003421219192" },
                    { 6, "Vladimir", "Lenin", null, "0895555232" }
                });

            migrationBuilder.InsertData(
                table: "Cars",
                columns: new[] { "CarId", "LicensePlate", "Model", "OwnerId", "TotalMillage" },
                values: new object[,]
                {
                    { 1, "SA9999AV", "Mercedess C200", 2, 59123.120000000003 },
                    { 5, "ST2959AS", "Peguo 350", 2, 102333.0 },
                    { 10, "А1123АС", "БМВ М3", 2, 199999.0 },
                    { 11, "CA4341AA", "Mercedess Smart", 2, 9123.1200000000008 },
                    { 6, "SA8999SA", "Audi RS", 3, 500.0 },
                    { 8, "СА7845АА", "Fiat Bravo", 3, 100002.12 },
                    { 9, "Б3288ББ", "BMW M6", 4, 10.199 },
                    { 4, "SA1111SA", "Fiat Panda", 5, 1000.01 },
                    { 3, "A3129SA", "VW GOLF", 6, 432432.90999999997 },
                    { 7, "RU1010RU", "Trabant", 6, 300000.0 }
                });

            migrationBuilder.InsertData(
                table: "Repairs",
                columns: new[] { "RepairId", "Bill", "CarId", "DateIn", "DateOut", "EmployeeId", "ProblemDescription" },
                values: new object[] { 3, null, 2, new DateTime(2019, 3, 6, 21, 23, 17, 699, DateTimeKind.Local).AddTicks(5943), null, 1, "Смяна Масло на скоростна кутия" });

            migrationBuilder.InsertData(
                table: "Repairs",
                columns: new[] { "RepairId", "Bill", "CarId", "DateIn", "DateOut", "EmployeeId", "ProblemDescription" },
                values: new object[] { 1, null, 1, new DateTime(2019, 3, 6, 21, 23, 17, 691, DateTimeKind.Local).AddTicks(2417), null, 5, "Смяна на спукана гума!" });

            migrationBuilder.InsertData(
                table: "Repairs",
                columns: new[] { "RepairId", "Bill", "CarId", "DateIn", "DateOut", "EmployeeId", "ProblemDescription" },
                values: new object[] { 4, null, 11, new DateTime(2019, 3, 6, 21, 23, 17, 699, DateTimeKind.Local).AddTicks(5953), null, 3, "Изхабени предни накладки" });

            migrationBuilder.InsertData(
                table: "Repairs",
                columns: new[] { "RepairId", "Bill", "CarId", "DateIn", "DateOut", "EmployeeId", "ProblemDescription" },
                values: new object[] { 5, null, 8, new DateTime(2019, 3, 6, 21, 23, 17, 699, DateTimeKind.Local).AddTicks(5963), null, 6, "Тунинг" });

            migrationBuilder.InsertData(
                table: "Repairs",
                columns: new[] { "RepairId", "Bill", "CarId", "DateIn", "DateOut", "EmployeeId", "ProblemDescription" },
                values: new object[] { 2, null, 4, new DateTime(2019, 3, 6, 21, 23, 17, 699, DateTimeKind.Local).AddTicks(5881), null, 2, "Десният прозорец не се отваря" });

            migrationBuilder.InsertData(
                table: "Repairs",
                columns: new[] { "RepairId", "Bill", "CarId", "DateIn", "DateOut", "EmployeeId", "ProblemDescription" },
                values: new object[] { 6, null, 7, new DateTime(2019, 3, 6, 21, 23, 17, 699, DateTimeKind.Local).AddTicks(5968), null, 4, "Годишен преглед" });
        }
    }
}
