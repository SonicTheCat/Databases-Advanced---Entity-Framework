using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GazServiz.Data.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Emoloyees",
                columns: table => new
                {
                    EmployeeId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 32, nullable: false),
                    Speciality = table.Column<int>(nullable: false, defaultValue: 4)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Emoloyees", x => x.EmployeeId);
                });

            migrationBuilder.CreateTable(
                name: "Owners",
                columns: table => new
                {
                    OwnerId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    FirstName = table.Column<string>(maxLength: 64, nullable: false),
                    MiddleName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(maxLength: 64, nullable: false),
                    Mobile = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Owners", x => x.OwnerId);
                    table.UniqueConstraint("AK_Owners_Mobile", x => x.Mobile);
                });

            migrationBuilder.CreateTable(
                name: "Cars",
                columns: table => new
                {
                    CarId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Model = table.Column<string>(maxLength: 100, nullable: false),
                    LicensePlate = table.Column<string>(nullable: false),
                    TotalMillage = table.Column<double>(nullable: false),
                    OwnerId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cars", x => x.CarId);
                    table.UniqueConstraint("AK_Cars_LicensePlate", x => x.LicensePlate);
                    table.ForeignKey(
                        name: "FK_Cars_Owners_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "Owners",
                        principalColumn: "OwnerId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Repairs",
                columns: table => new
                {
                    RepairId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    IsFixed = table.Column<bool>(nullable: false, defaultValue: false),
                    Bill = table.Column<decimal>(nullable: true),
                    ProblemDescription = table.Column<string>(maxLength: 1000, nullable: true),
                    DateIn = table.Column<DateTime>(nullable: false),
                    DateOut = table.Column<DateTime>(nullable: true),
                    CarId = table.Column<int>(nullable: false),
                    EmployeeId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Repairs", x => x.RepairId);
                    table.ForeignKey(
                        name: "FK_Repairs_Cars_CarId",
                        column: x => x.CarId,
                        principalTable: "Cars",
                        principalColumn: "CarId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Repairs_Emoloyees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Emoloyees",
                        principalColumn: "EmployeeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Emoloyees",
                columns: new[] { "EmployeeId", "Name" },
                values: new object[] { 1, "Bat Pesho" });

            migrationBuilder.InsertData(
                table: "Emoloyees",
                columns: new[] { "EmployeeId", "Name", "Speciality" },
                values: new object[,]
                {
                    { 2, "Boiko Borisov", 4 },
                    { 3, "Moni Todorov", 1 },
                    { 4, "Petar Petrov", 5 },
                    { 5, "Mishelin Georgiev", 3 },
                    { 6, "Bai Ivan", 2 }
                });

            migrationBuilder.InsertData(
                table: "Owners",
                columns: new[] { "OwnerId", "FirstName", "LastName", "MiddleName", "Mobile" },
                values: new object[,]
                {
                    { 1, "George", "Lukas", null, "089877321" },
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
                    { 2, "PP1010AA", "Honda Accord", 1, 100000.0 },
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
                values: new object[] { 3, null, 2, new DateTime(2019, 3, 6, 20, 47, 44, 596, DateTimeKind.Local).AddTicks(9912), null, 1, "Смяна Масло на скоростна кутия" });

            migrationBuilder.InsertData(
                table: "Repairs",
                columns: new[] { "RepairId", "Bill", "CarId", "DateIn", "DateOut", "EmployeeId", "ProblemDescription" },
                values: new object[] { 1, null, 1, new DateTime(2019, 3, 6, 20, 47, 44, 581, DateTimeKind.Local).AddTicks(3874), null, 5, "Смяна на спукана гума!" });

            migrationBuilder.InsertData(
                table: "Repairs",
                columns: new[] { "RepairId", "Bill", "CarId", "DateIn", "DateOut", "EmployeeId", "ProblemDescription" },
                values: new object[] { 4, null, 11, new DateTime(2019, 3, 6, 20, 47, 44, 596, DateTimeKind.Local).AddTicks(9989), null, 3, "Изхабени предни накладки" });

            migrationBuilder.InsertData(
                table: "Repairs",
                columns: new[] { "RepairId", "Bill", "CarId", "DateIn", "DateOut", "EmployeeId", "ProblemDescription" },
                values: new object[] { 5, null, 8, new DateTime(2019, 3, 6, 20, 47, 44, 597, DateTimeKind.Local).AddTicks(15), null, 6, "Тунинг" });

            migrationBuilder.InsertData(
                table: "Repairs",
                columns: new[] { "RepairId", "Bill", "CarId", "DateIn", "DateOut", "EmployeeId", "ProblemDescription" },
                values: new object[] { 2, null, 4, new DateTime(2019, 3, 6, 20, 47, 44, 596, DateTimeKind.Local).AddTicks(9727), null, 2, "Десният прозорец не се отваря" });

            migrationBuilder.InsertData(
                table: "Repairs",
                columns: new[] { "RepairId", "Bill", "CarId", "DateIn", "DateOut", "EmployeeId", "ProblemDescription" },
                values: new object[] { 6, null, 7, new DateTime(2019, 3, 6, 20, 47, 44, 597, DateTimeKind.Local).AddTicks(66), null, 4, "Годишен преглед" });

            migrationBuilder.CreateIndex(
                name: "IX_Cars_OwnerId",
                table: "Cars",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_Repairs_CarId",
                table: "Repairs",
                column: "CarId");

            migrationBuilder.CreateIndex(
                name: "IX_Repairs_EmployeeId",
                table: "Repairs",
                column: "EmployeeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Repairs");

            migrationBuilder.DropTable(
                name: "Cars");

            migrationBuilder.DropTable(
                name: "Emoloyees");

            migrationBuilder.DropTable(
                name: "Owners");
        }
    }
}
