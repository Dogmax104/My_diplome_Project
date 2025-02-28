using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Arctech_Manufaction_Menedgment.Migrations
{
    /// <inheritdoc />
    public partial class migration1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ProgectModels",
                columns: table => new
                {
                    IdProjectModel = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    NameProjectModel = table.Column<string>(type: "TEXT", nullable: false),
                    ClientNameProjectModel = table.Column<string>(type: "TEXT", nullable: false),
                    BeginTime = table.Column<DateTime>(type: "TEXT", nullable: false),
                    CoordinationTime = table.Column<DateTime>(type: "TEXT", nullable: true),
                    CoordinationFileProjectModel = table.Column<byte>(type: "INTEGER", nullable: true),
                    OrderInManufaction = table.Column<bool>(type: "INTEGER", nullable: true),
                    StatusOrder = table.Column<int>(type: "INTEGER", nullable: true),
                    NotesProjectModel = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProgectModels", x => x.IdProjectModel);
                });

            migrationBuilder.CreateTable(
                name: "UserArctechs",
                columns: table => new
                {
                    NameUser = table.Column<string>(type: "TEXT", nullable: false),
                    PasswordUser = table.Column<string>(type: "TEXT", nullable: false),
                    Id = table.Column<int>(type: "INTEGER", nullable: false),
                    RoleUser = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserArctechs", x => new { x.NameUser, x.PasswordUser });
                });

            migrationBuilder.CreateTable(
                name: "ModelFiles",
                columns: table => new
                {
                    IdModelFileClient = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    NameModelFileClient = table.Column<string>(type: "TEXT", nullable: false),
                    NameModelFile = table.Column<byte[]>(type: "BLOB", nullable: false),
                    DateModelFile = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ProgectModelIdProjectModel = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ModelFiles", x => x.IdModelFileClient);
                    table.ForeignKey(
                        name: "FK_ModelFiles_ProgectModels_ProgectModelIdProjectModel",
                        column: x => x.ProgectModelIdProjectModel,
                        principalTable: "ProgectModels",
                        principalColumn: "IdProjectModel");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ModelFiles_ProgectModelIdProjectModel",
                table: "ModelFiles",
                column: "ProgectModelIdProjectModel");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ModelFiles");

            migrationBuilder.DropTable(
                name: "UserArctechs");

            migrationBuilder.DropTable(
                name: "ProgectModels");
        }
    }
}
