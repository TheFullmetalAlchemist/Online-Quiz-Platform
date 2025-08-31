using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Online_Quiz_Platform.Migrations
{
    /// <inheritdoc />
    public partial class AddUsernameToQuizAttempts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UserEmail",
                table: "QuizAttempts",
                newName: "UserName");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UserName",
                table: "QuizAttempts",
                newName: "UserEmail");
        }
    }
}
