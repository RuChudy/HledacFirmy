using System;
using Hledac.Database.Context;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hledac.Database.Migrations
{
    /// <inheritdoc />
    public partial class DataForVr : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SubjectVrSet",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ICO = table.Column<string>(type: "nvarchar(8)", maxLength: 8, nullable: false),
                    PravniForma = table.Column<int>(type: "int", nullable: true),
                    DatumPosledniKontroly = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Rejstrik = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    RequestError = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ResponseError = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Aktualizace_DB = table.Column<DateTime>(type: "datetime2", nullable: true),
                    TypVypisu = table.Column<string>(type: "nvarchar(52)", maxLength: 52, nullable: true),
                    S_StavSubjektu = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    S_Konkurz = table.Column<int>(type: "int", nullable: true),
                    S_Vyrovnani = table.Column<int>(type: "int", nullable: true),
                    S_Zamitnuti = table.Column<int>(type: "int", nullable: true),
                    S_Likvidace = table.Column<int>(type: "int", nullable: true),
                    ObchodniFirma = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: false),
                    Jmeno = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Prijmeni = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    DatumNarozeni = table.Column<DateTime>(type: "datetime2", nullable: true),
                    PF_Kody = table.Column<int>(type: "int", nullable: true),
                    PF_Nazev = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    PF_Osoba = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    PF_Text = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    A_IDAdresy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    A_KodStatu = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    A_NazevStatu = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    A_NazevOkresu = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    A_NazevObce = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    A_NazevCastiObce = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    A_NazevUlice = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    A_CisloDomovni = table.Column<int>(type: "int", nullable: true),
                    A_TypCisloDomovni = table.Column<int>(type: "int", nullable: true),
                    A_CisloOrientacni = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    A_PSC = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    DatumZapisu = table.Column<DateTime>(type: "datetime2", nullable: true),
                    MistoZapisu = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ZnackaZapisu = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubjectVrSet", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SubjectVrSet_ICO",
                table: "SubjectVrSet",
                column: "ICO");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SubjectVrSet");
        }
    }
}
