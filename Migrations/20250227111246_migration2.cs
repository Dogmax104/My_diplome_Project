using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Arctech_Manufaction_Menedgment.Migrations
{
    /// <inheritdoc />
    public partial class migration2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ModelFiles_ProgectModels_ProgectModelIdProjectModel",
                table: "ModelFiles");

            migrationBuilder.DropIndex(
                name: "IX_ModelFiles_ProgectModelIdProjectModel",
                table: "ModelFiles");

            migrationBuilder.DropColumn(
                name: "ProgectModelIdProjectModel",
                table: "ModelFiles");

            migrationBuilder.AddColumn<int>(
                name: "IdProjectModel",
                table: "ModelFiles",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_ModelFiles_IdProjectModel",
                table: "ModelFiles",
                column: "IdProjectModel");

            migrationBuilder.AddForeignKey(
                name: "FK_ModelFiles_ProgectModels_IdProjectModel",
                table: "ModelFiles",
                column: "IdProjectModel",
                principalTable: "ProgectModels",
                principalColumn: "IdProjectModel",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ModelFiles_ProgectModels_IdProjectModel",
                table: "ModelFiles");

            migrationBuilder.DropIndex(
                name: "IX_ModelFiles_IdProjectModel",
                table: "ModelFiles");

            migrationBuilder.DropColumn(
                name: "IdProjectModel",
                table: "ModelFiles");

            migrationBuilder.AddColumn<int>(
                name: "ProgectModelIdProjectModel",
                table: "ModelFiles",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ModelFiles_ProgectModelIdProjectModel",
                table: "ModelFiles",
                column: "ProgectModelIdProjectModel");

            migrationBuilder.AddForeignKey(
                name: "FK_ModelFiles_ProgectModels_ProgectModelIdProjectModel",
                table: "ModelFiles",
                column: "ProgectModelIdProjectModel",
                principalTable: "ProgectModels",
                principalColumn: "IdProjectModel");
        }
    }
}
