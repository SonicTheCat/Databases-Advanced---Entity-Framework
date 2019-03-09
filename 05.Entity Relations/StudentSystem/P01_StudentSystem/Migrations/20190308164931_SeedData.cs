using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace P01_StudentSystem.Migrations
{
    public partial class SeedData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Courses",
                columns: new[] { "CourseId", "Description", "EndDate", "Name", "Price", "StartDate" },
                values: new object[,]
                {
                    { 1, null, new DateTime(2018, 1, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), "C# OOP", 490m, new DateTime(2018, 9, 17, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 2, null, new DateTime(2018, 10, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "Java Web", 490m, new DateTime(2018, 9, 20, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 3, null, new DateTime(2018, 10, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "JS Core", 490m, new DateTime(2018, 9, 15, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.InsertData(
                table: "Students",
                columns: new[] { "StudentId", "Birthday", "Name", "PhoneNumber", "RegisteredOn" },
                values: new object[,]
                {
                    { 1, null, "Sashka", "0894292919", new DateTime(2010, 10, 12, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 2, null, "Milka", "0888101768", new DateTime(2015, 6, 10, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 3, null, "Penka", "02/542 132", new DateTime(2011, 11, 11, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.InsertData(
                table: "HomeworkSubmissions",
                columns: new[] { "HomeworkId", "Content", "ContentType", "CourseId", "StudentId", "SubmissionTime" },
                values: new object[,]
                {
                    { 1, "kvi sa tez gluposti", 0, 1, 1, new DateTime(2010, 10, 10, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 2, "blq blq blq...", 1, 1, 2, new DateTime(2010, 10, 10, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 3, "javascript", 2, 3, 3, new DateTime(2010, 10, 10, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.InsertData(
                table: "Resources",
                columns: new[] { "ResourceId", "CourseId", "Name", "ResourceType", "Url" },
                values: new object[] { 1, 1, "The best book for c# developers", 2, "www.php.com" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "CourseId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "HomeworkSubmissions",
                keyColumn: "HomeworkId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "HomeworkSubmissions",
                keyColumn: "HomeworkId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "HomeworkSubmissions",
                keyColumn: "HomeworkId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Resources",
                keyColumn: "ResourceId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "CourseId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "CourseId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "StudentId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "StudentId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "StudentId",
                keyValue: 3);
        }
    }
}
