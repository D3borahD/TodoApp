using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class updateStep : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Steps_ProjectTypes_TypeId",
                table: "Steps");

            migrationBuilder.DropIndex(
                name: "IX_Steps_TypeId",
                table: "Steps");

            migrationBuilder.RenameColumn(
                name: "TypeId",
                table: "Steps",
                newName: "Type");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Type",
                table: "Steps",
                newName: "TypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Steps_TypeId",
                table: "Steps",
                column: "TypeId");

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
