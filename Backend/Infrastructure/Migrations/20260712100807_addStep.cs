using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addStep : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StepDao_ProjectTypes_TypeId",
                table: "StepDao");

            migrationBuilder.DropForeignKey(
                name: "FK_StepDao_Projects_ProjectId",
                table: "StepDao");

            migrationBuilder.DropPrimaryKey(
                name: "PK_StepDao",
                table: "StepDao");

            migrationBuilder.RenameTable(
                name: "StepDao",
                newName: "Steps");

            migrationBuilder.RenameIndex(
                name: "IX_StepDao_TypeId",
                table: "Steps",
                newName: "IX_Steps_TypeId");

            migrationBuilder.RenameIndex(
                name: "IX_StepDao_ProjectId",
                table: "Steps",
                newName: "IX_Steps_ProjectId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Steps",
                table: "Steps",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Steps_ProjectTypes_TypeId",
                table: "Steps",
                column: "TypeId",
                principalTable: "ProjectTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Steps_Projects_ProjectId",
                table: "Steps",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Steps_ProjectTypes_TypeId",
                table: "Steps");

            migrationBuilder.DropForeignKey(
                name: "FK_Steps_Projects_ProjectId",
                table: "Steps");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Steps",
                table: "Steps");

            migrationBuilder.RenameTable(
                name: "Steps",
                newName: "StepDao");

            migrationBuilder.RenameIndex(
                name: "IX_Steps_TypeId",
                table: "StepDao",
                newName: "IX_StepDao_TypeId");

            migrationBuilder.RenameIndex(
                name: "IX_Steps_ProjectId",
                table: "StepDao",
                newName: "IX_StepDao_ProjectId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_StepDao",
                table: "StepDao",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_StepDao_ProjectTypes_TypeId",
                table: "StepDao",
                column: "TypeId",
                principalTable: "ProjectTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StepDao_Projects_ProjectId",
                table: "StepDao",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
