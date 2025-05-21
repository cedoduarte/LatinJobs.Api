using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LatinJobs.Api.Migrations
{
    /// <inheritdoc />
    public partial class UserAuthentication : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Authentications_Users_UserId",
                table: "Authentications");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Authentications",
                table: "Authentications");

            migrationBuilder.RenameTable(
                name: "Authentications",
                newName: "UserAuthentications");

            migrationBuilder.RenameIndex(
                name: "IX_Authentications_UserId",
                table: "UserAuthentications",
                newName: "IX_UserAuthentications_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserAuthentications",
                table: "UserAuthentications",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserAuthentications_Users_UserId",
                table: "UserAuthentications",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserAuthentications_Users_UserId",
                table: "UserAuthentications");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserAuthentications",
                table: "UserAuthentications");

            migrationBuilder.RenameTable(
                name: "UserAuthentications",
                newName: "Authentications");

            migrationBuilder.RenameIndex(
                name: "IX_UserAuthentications_UserId",
                table: "Authentications",
                newName: "IX_Authentications_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Authentications",
                table: "Authentications",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Authentications_Users_UserId",
                table: "Authentications",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
