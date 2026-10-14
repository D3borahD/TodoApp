using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class updateProjectTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ProjectDaoId",
                table: "ProjectTypes",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProjectTypes_ProjectDaoId",
                table: "ProjectTypes",
                column: "ProjectDaoId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectTypes_Projects_ProjectDaoId",
                table: "ProjectTypes",
                column: "ProjectDaoId",
                principalTable: "Projects",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProjectTypes_Projects_ProjectDaoId",
                table: "ProjectTypes");

            migrationBuilder.DropIndex(
                name: "IX_ProjectTypes_ProjectDaoId",
                table: "ProjectTypes");

            migrationBuilder.DropColumn(
                name: "ProjectDaoId",
                table: "ProjectTypes");
        }
    }
}
