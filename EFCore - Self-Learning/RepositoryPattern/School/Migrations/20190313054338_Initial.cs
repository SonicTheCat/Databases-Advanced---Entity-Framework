using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SoftUni.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Tags",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tags", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Teachers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 64, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Teachers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Courses",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: false),
                    Level = table.Column<int>(nullable: false),
                    Description = table.Column<string>(maxLength: 1500, nullable: false),
                    FullPrice = table.Column<decimal>(nullable: false),
                    TeacherId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Courses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Courses_Teachers_TeacherId",
                        column: x => x.TeacherId,
                        principalTable: "Teachers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CourseTag",
                columns: table => new
                {
                    TagId = table.Column<int>(nullable: false),
                    CourseId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseTag", x => new { x.TagId, x.CourseId });
                    table.ForeignKey(
                        name: "FK_CourseTag_Courses_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Courses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CourseTag_Tags_TagId",
                        column: x => x.TagId,
                        principalTable: "Tags",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Tags",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "c#" },
                    { 2, "angularjs" },
                    { 3, "javascript" },
                    { 4, "nodejs" },
                    { 5, "oop" },
                    { 6, "linq" }
                });

            migrationBuilder.InsertData(
                table: "Teachers",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Mosh Hamedani" },
                    { 2, "Anthony Alicea" },
                    { 3, "Eric Wise" },
                    { 4, "Tom Owsiak" },
                    { 5, "John Smith" }
                });

            migrationBuilder.InsertData(
                table: "Courses",
                columns: new[] { "Id", "Description", "FullPrice", "Level", "Name", "TeacherId" },
                values: new object[,]
                {
                    { 1, "Description for C# Basics", 49m, 1, "C# Basics", 1 },
                    { 4, "Description for Javascript", 149m, 2, "Javascript: Understanding the Weird Parts", 1 },
                    { 7, "Description for Programming for Beginners", 45m, 1, "Programming for Complete Beginners", 1 },
                    { 6, "Description for NodeJS", 149m, 2, "Learn and Understand NodeJS", 2 },
                    { 5, "Description for AngularJS", 99m, 2, "Learn and Understand AngularJS", 3 },
                    { 8, "Description 16 Hour Course", 150m, 1, "A 16 Hour C# Course with Visual Studio 2013", 3 },
                    { 2, "Description for C# Intermediate", 49m, 2, "C# Intermediate", 4 },
                    { 9, "Description Learn Javascript", 20m, 1, "Learn JavaScript Through Visual Studio 2013", 4 },
                    { 3, "Description for C# Advanced", 69m, 3, "C# Advanced", 5 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Courses_TeacherId",
                table: "Courses",
                column: "TeacherId");

            migrationBuilder.CreateIndex(
                name: "IX_CourseTag_CourseId",
                table: "CourseTag",
                column: "CourseId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CourseTag");

            migrationBuilder.DropTable(
                name: "Courses");

            migrationBuilder.DropTable(
                name: "Tags");

            migrationBuilder.DropTable(
                name: "Teachers");
        }
    }
}
