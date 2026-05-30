using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class updateProject : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Projects_ProjectTypes_TypeId",
                table: "Projects");

            migrationBuilder.DropForeignKey(
                name: "FK_Projects_StepDao_StepId",
                table: "Projects");

            migrationBuilder.DropIndex(
                name: "IX_Projects_StepId",
                table: "Projects");

            migrationBuilder.DropIndex(
                name: "IX_Projects_TypeId",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "StepId",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "TypeId",
                table: "Projects");

            migrationBuilder.AddColumn<int>(
                name: "ProjectDaoId",
                table: "StepDao",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_StepDao_ProjectDaoId",
                table: "StepDao",
                column: "ProjectDaoId");

            migrationBuilder.AddForeignKey(
                name: "FK_StepDao_Projects_ProjectDaoId",
                table: "StepDao",
                column: "ProjectDaoId",
                principalTable: "Projects",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StepDao_Projects_ProjectDaoId",
                table: "StepDao");

            migrationBuilder.DropIndex(
                name: "IX_StepDao_ProjectDaoId",
                table: "StepDao");

            migrationBuilder.DropColumn(
                name: "ProjectDaoId",
                table: "StepDao");

            migrationBuilder.AddColumn<int>(
                name: "StepId",
                table: "Projects",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TypeId",
                table: "Projects",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Projects_StepId",
                table: "Projects",
                column: "StepId");

            migrationBuilder.CreateIndex(
                name: "IX_Projects_TypeId",
                table: "Projects",
                column: "TypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Projects_ProjectTypes_TypeId",
                table: "Projects",
                column: "TypeId",
                principalTable: "ProjectTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Projects_StepDao_StepId",
                table: "Projects",
                column: "StepId",
                principalTable: "StepDao",
                principalColumn: "Id");
        }
    }
}
