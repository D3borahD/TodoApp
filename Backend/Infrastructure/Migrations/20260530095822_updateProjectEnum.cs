using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class updateProjectEnum : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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
                name: "ProjectId",
                table: "StepDao",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_StepDao_ProjectId",
                table: "StepDao",
                column: "ProjectId");

            migrationBuilder.AddForeignKey(
                name: "FK_StepDao_Projects_ProjectId",
                table: "StepDao",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StepDao_Projects_ProjectId",
                table: "StepDao");

            migrationBuilder.DropIndex(
                name: "IX_StepDao_ProjectId",
                table: "StepDao");

            migrationBuilder.DropColumn(
                name: "ProjectId",
                table: "StepDao");

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
    }
}
