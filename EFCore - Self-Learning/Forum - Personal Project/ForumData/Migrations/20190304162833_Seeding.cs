using Microsoft.EntityFrameworkCore.Migrations;

namespace ForumData.Migrations
{
    public partial class Seeding : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "CategoryId", "Name" },
                values: new object[,]
                {
                    { 1, "Music" },
                    { 2, "Infromation" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserID", "Status", "Username" },
                values: new object[,]
                {
                    { 1, "Top User", "Mark Zukenberg" },
                    { 2, "New", "Jay-Z" },
                    { 3, "Star", "Beyonce" },
                    { 4, "Vintage", "Lili Ivanova" },
                    { 5, "Unknown", "Kichka Bodurova" }
                });

            migrationBuilder.InsertData(
                table: "Posts",
                columns: new[] { "PostID", "AuthorId", "CategoryId", "Content", "Title" },
                values: new object[] { 1, 1, 2, "Please read this message till the end! blq blq blq...", "New Privacy Policy in FB" });

            migrationBuilder.InsertData(
                table: "Posts",
                columns: new[] { "PostID", "AuthorId", "CategoryId", "Content", "Title" },
                values: new object[] { 2, 3, 1, "Listen to my brand new single - 'Самотни лелки' ", "!!!NEW SINGLE!!!" });

            migrationBuilder.InsertData(
                table: "Posts",
                columns: new[] { "PostID", "AuthorId", "CategoryId", "Content", "Title" },
                values: new object[] { 3, 4, 1, "Примата на българската естрада организира грандиозен конц...", "300 Години на Сцена" });

            migrationBuilder.InsertData(
                table: "Comments",
                columns: new[] { "CommentID", "AuthorId", "Content", "PostId" },
                values: new object[,]
                {
                    { 1, 2, "tam sam!", 1 },
                    { 2, 5, "qko heit...", 1 },
                    { 3, 1, "kolko struva ?", 1 },
                    { 4, 3, "bez pari e we, kaltak ! Teaser", 1 }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Comments",
                keyColumn: "CommentID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Comments",
                keyColumn: "CommentID",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Comments",
                keyColumn: "CommentID",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Comments",
                keyColumn: "CommentID",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Posts",
                keyColumn: "PostID",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Posts",
                keyColumn: "PostID",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Posts",
                keyColumn: "PostID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 1);
        }
    }
}
