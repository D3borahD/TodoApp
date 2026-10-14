using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class FixPojectTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TypeDaoId",
                table: "Projects",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Projects_TypeDaoId",
                table: "Projects",
                column: "TypeDaoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Projects_ProjectTypes_TypeDaoId",
                table: "Projects",
                column: "TypeDaoId",
                principalTable: "ProjectTypes",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Projects_ProjectTypes_TypeDaoId",
                table: "Projects");

            migrationBuilder.DropIndex(
                name: "IX_Projects_TypeDaoId",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "TypeDaoId",
                table: "Projects");
        }
    }
}
