using System;
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
                name: "RssCacheFeeds",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Deleted = table.Column<DateTime>(type: "datetime2", nullable: true),
                    SiteUri = table.Column<string>(type: "nvarchar(900)", maxLength: 900, nullable: false),
                    Link = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Language = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Copyright = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RssCacheFeeds", x => x.Id);
                });

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

            migrationBuilder.CreateTable(
                name: "RssCacheFeedItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FeedId = table.Column<int>(type: "int", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PublishDateUtc = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Guid = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Link = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Body = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OtherProperties_Categories = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RssCacheFeedItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RssCacheFeedItems_RssCacheFeeds_FeedId",
                        column: x => x.FeedId,
                        principalTable: "RssCacheFeeds",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RssCacheFeedItems_FeedId",
                table: "RssCacheFeedItems",
                column: "FeedId");

            migrationBuilder.CreateIndex(
                name: "IX_RssCacheFeeds_SiteUri",
                table: "RssCacheFeeds",
                column: "SiteUri",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Subjekty_Ico",
                table: "Subjekty",
                column: "Ico");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RssCacheFeedItems");

            migrationBuilder.DropTable(
                name: "Subjekty");

            migrationBuilder.DropTable(
                name: "RssCacheFeeds");
        }
    }
}
