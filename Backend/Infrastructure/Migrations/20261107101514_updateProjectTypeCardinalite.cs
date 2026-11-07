using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class updateProjectTypeCardinalite : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Projects_ProjectTypes_TypeDaoId",
                table: "Projects");

            migrationBuilder.DropForeignKey(
                name: "FK_Steps_ProjectTypes_TypeId",
                table: "Steps");

            migrationBuilder.DropIndex(
                name: "IX_Steps_TypeId",
                table: "Steps");

            migrationBuilder.DropIndex(
                name: "IX_Projects_TypeDaoId",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "TypeDaoId",
                table: "Projects");

            migrationBuilder.AddColumn<int>(
                name: "ProjectTypeId",
                table: "Projects",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProjectTypeId",
                table: "Projects");

            migrationBuilder.AddColumn<int>(
                name: "TypeDaoId",
                table: "Projects",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Steps_TypeId",
                table: "Steps",
                column: "TypeId");

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

            migrationBuilder.AddForeignKey(
                name: "FK_Steps_ProjectTypes_TypeId",
                table: "Steps",
                column: "TypeId",
                principalTable: "ProjectTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
