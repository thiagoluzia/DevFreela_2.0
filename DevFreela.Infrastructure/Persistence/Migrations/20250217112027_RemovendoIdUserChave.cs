using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DevFreela.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class RemovendoIdUserChave : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProjectComments_Users_USerId",
                table: "ProjectComments");

            migrationBuilder.DropIndex(
                name: "IX_ProjectComments_USerId",
                table: "ProjectComments");

            migrationBuilder.DropColumn(
                name: "USerId",
                table: "ProjectComments");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectComments_IdUSer",
                table: "ProjectComments",
                column: "IdUSer");

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectComments_Users_IdUSer",
                table: "ProjectComments",
                column: "IdUSer",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProjectComments_Users_IdUSer",
                table: "ProjectComments");

            migrationBuilder.DropIndex(
                name: "IX_ProjectComments_IdUSer",
                table: "ProjectComments");

            migrationBuilder.AddColumn<int>(
                name: "USerId",
                table: "ProjectComments",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_ProjectComments_USerId",
                table: "ProjectComments",
                column: "USerId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectComments_Users_USerId",
                table: "ProjectComments",
                column: "USerId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
