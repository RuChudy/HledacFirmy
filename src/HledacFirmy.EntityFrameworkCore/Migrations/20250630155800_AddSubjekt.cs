using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HledacFirmy.Migrations
{
    /// <inheritdoc />
    public partial class AddSubjekt : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ApplicationName",
                table: "AbpBackgroundJobs",
                type: "nvarchar(96)",
                maxLength: 96,
                nullable: true);

            migrationBuilder.CreateTable(
                name: "AppSubjekt",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Ico = table.Column<string>(type: "nvarchar(8)", maxLength: 8, nullable: false),
                    ObchJmeno = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Dic = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    Stat = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Kraj = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Okres = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Obec = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Obvod = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Ulice = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    CisloDomovni = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    CisloOrientacni = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Psc = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: true),
                    DorucovaciAdresa1 = table.Column<string>(type: "nvarchar(max)", maxLength: -1, nullable: true),
                    DorucovaciAdresa2 = table.Column<string>(type: "nvarchar(max)", maxLength: -1, nullable: true),
                    DorucovaciAdresa3 = table.Column<string>(type: "nvarchar(max)", maxLength: -1, nullable: true),
                    DatumVzniku = table.Column<DateTime>(type: "date", nullable: false),
                    DatumZaniku = table.Column<DateTime>(type: "date", nullable: true),
                    DatumAktualizace = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", maxLength: -1, nullable: true),
                    ExtraProperties = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    DeleterId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DeletionTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppSubjekt", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppSubjekt");

            migrationBuilder.DropColumn(
                name: "ApplicationName",
                table: "AbpBackgroundJobs");
        }
    }
}
