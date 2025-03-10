using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Arctech_Manufaction_Menedgment.Migrations
{
    /// <inheritdoc />
    public partial class addDatabase : Migration
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
                    CoordinationTime = table.Column<DateTime>(type: "TEXT", nullable: false),
                    CoordinationFileProjectModel = table.Column<byte>(type: "INTEGER", nullable: true),
                    OrderInManufaction = table.Column<bool>(type: "INTEGER", nullable: false),
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
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Namer_for_project = table.Column<int>(type: "INTEGER", nullable: false),
                    FistNameUser = table.Column<string>(type: "TEXT", nullable: false),
                    SecondNameUser = table.Column<string>(type: "TEXT", nullable: false),
                    NameUser = table.Column<string>(type: "TEXT", nullable: false),
                    PasswordUser = table.Column<string>(type: "TEXT", nullable: false),
                    BeginUser = table.Column<DateTime>(type: "TEXT", nullable: false),
                    RoleUser = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserArctechs", x => x.Id);
                    table.UniqueConstraint("AK_UserArctechs_NameUser_PasswordUser", x => new { x.NameUser, x.PasswordUser });
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
                    IdProjectModel104 = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ModelFiles", x => x.IdModelFileClient);
                    table.ForeignKey(
                        name: "FK_ModelFiles_ProgectModels_IdProjectModel104",
                        column: x => x.IdProjectModel104,
                        principalTable: "ProgectModels",
                        principalColumn: "IdProjectModel",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ModelFiles_IdProjectModel104",
                table: "ModelFiles",
                column: "IdProjectModel104");
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
