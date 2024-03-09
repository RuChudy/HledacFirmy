using System;
using Hledac.Database.Context;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hledac.Database.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Subjekty",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Deleted = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Ico = table.Column<string>(type: "nvarchar(8)", maxLength: 8, nullable: false),
                    ObchJmeno = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: false),
                    Dic = table.Column<string>(type: "nvarchar(12)", maxLength: 12, nullable: true),
                    Stat = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Kraj = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Okres = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Obec = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Obvod = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ulice = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CisloDomovni = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CisloOrientacni = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Psc = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DorucovaciAdresa1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DorucovaciAdresa2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DorucovaciAdresa3 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DatumVzniku = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DatumZaniku = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DatumAktualizace = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subjekty", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Subjekty_Ico",
                table: "Subjekty",
                column: "Ico");

            migrationBuilder.Sql(SubjektDbContext.CreateSqlProc_SubjektSave_V1());
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Subjekty");
        }
    }
}
