using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateSavedCourse : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_SavedCourses",
                table: "SavedCourses");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "SavedCourses",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "SavedCourses",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SavedCourses",
                table: "SavedCourses",
                column: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_SavedCourses",
                table: "SavedCourses");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "SavedCourses");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "SavedCourses",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SavedCourses",
                table: "SavedCourses",
                columns: new[] { "UserId", "CourseId" });
        }
    }
}
