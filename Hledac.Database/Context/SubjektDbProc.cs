using System.Data;
using Microsoft.Data.SqlClient;

namespace Hledac.Database.Context;

public partial class SubjektDbContext
{
    /// <summary>
    /// Vytvoří nebo aktualizuje subjekt přez proceduru Subjekt_Save.
    /// </summary>
    /// <returns>Záznam z tabulky Subjekt</returns>
    public Subjekt? SaveSubjekt(string ico, string? dic, string jmeno,
        string? stat, string? kraj, string? okres, string? obec, string? obvod, string? ulice, string? cisloDomovni, string? cisloOrientacni, string? psc,
        string? dorucovaciAdresa1, string? dorucovaciAdresa2, string? dorucovaciAdresa3,
        DateTime? vznik, DateTime? zanik, DateTime? aktualizace, string? description)
    {
        Subjekt? result = Subjekty
            .FromSqlRaw("""
                EXECUTE [dbo].[Subjekt_Save] @Ico, @Dic, @ObchJmeno,
                @Stat, @Kraj, @Okres, @Obec, @Obvod, @Ulice, @CisloDomovni, @CisloOrientacni, @Psc,
                @DorucovaciAdresa1, @DorucovaciAdresa2, @DorucovaciAdresa3,
                @DatumVzniku, @DatumZaniku, @DatumAktualizace, @Description
                """,
                new SqlParameter("@Ico", SqlDbType.NVarChar, 8) { Value = ico },
                new SqlParameter("@Dic", SqlDbType.NVarChar, 12) { Value = dic ?? (object)DBNull.Value },
                new SqlParameter("@ObchJmeno", SqlDbType.NVarChar, -1) { Value = jmeno },
                new SqlParameter("@Stat", SqlDbType.NVarChar, -1) { Value = stat ?? (object)DBNull.Value },
                new SqlParameter("@Kraj", SqlDbType.NVarChar, -1) { Value = kraj ?? (object)DBNull.Value },
                new SqlParameter("@Okres", SqlDbType.NVarChar, -1) { Value = okres ?? (object)DBNull.Value },
                new SqlParameter("@Obec", SqlDbType.NVarChar, -1) { Value = obec ?? (object)DBNull.Value },
                new SqlParameter("@Obvod", SqlDbType.NVarChar, -1) { Value = obvod ?? (object)DBNull.Value },
                new SqlParameter("@Ulice", SqlDbType.NVarChar, -1) { Value = ulice ?? (object)DBNull.Value },
                new SqlParameter("@CisloDomovni", SqlDbType.NVarChar, -1) { Value = cisloDomovni ?? (object)DBNull.Value },
                new SqlParameter("@CisloOrientacni", SqlDbType.NVarChar, -1) { Value = cisloOrientacni ?? (object)DBNull.Value },
                new SqlParameter("@Psc", SqlDbType.NVarChar, -1) { Value = psc ?? (object)DBNull.Value },
                new SqlParameter("@DorucovaciAdresa1", SqlDbType.NVarChar, -1) { Value = dorucovaciAdresa1 ?? (object)DBNull.Value },
                new SqlParameter("@DorucovaciAdresa2", SqlDbType.NVarChar, -1) { Value = dorucovaciAdresa2 ?? (object)DBNull.Value },
                new SqlParameter("@DorucovaciAdresa3", SqlDbType.NVarChar, -1) { Value = dorucovaciAdresa3 ?? (object)DBNull.Value },
                new SqlParameter("@DatumVzniku", SqlDbType.DateTime) { Value = vznik ?? (object)DBNull.Value },
                new SqlParameter("@DatumZaniku", SqlDbType.DateTime) { Value = zanik ?? (object)DBNull.Value },
                new SqlParameter("@DatumAktualizace", SqlDbType.DateTime) { Value = aktualizace ?? (object)DBNull.Value },
                new SqlParameter("@Description", SqlDbType.NVarChar, -1) { Value = description ?? (object)DBNull.Value }
            )
            .AsNoTracking()
            .AsEnumerable()
            .FirstOrDefault();

        return result;
    }

    /// <summary>
    /// Definice SQL procedury.
    /// </summary>
    /// <returns>Definice SQL procedury.</returns>
    public static string CreateSqlProc_SubjektSave_V1()
    {
        // migrationBuilder.Sql(SubjektDbContext.CreateSqlProc_SubjektSave_V1());

        var sp = """
                CREATE PROCEDURE [dbo].[Subjekt_Save]
                    @Ico nvarchar(8),
                    @Dic nvarchar(12),
                    @ObchJmeno nvarchar(max),
                    @Stat nvarchar(max),
                    @Kraj nvarchar(max),
                    @Okres nvarchar(max),
                    @Obec nvarchar(max),
                    @Obvod nvarchar(max),
                    @Ulice nvarchar(max),
                    @CisloDomovni nvarchar(max),
                    @CisloOrientacni nvarchar(max),
                    @Psc nvarchar(max),
                    @DorucovaciAdresa1 nvarchar(max),
                    @DorucovaciAdresa2 nvarchar(max),
                    @DorucovaciAdresa3 nvarchar(max),
                    @DatumVzniku datetime,
                    @DatumZaniku datetime,
                    @DatumAktualizace datetime,
                    @Description nvarchar(max)
                AS
                BEGIN
                    SET NOCOUNT ON;
                    DECLARE @Exist int;

                    SET @Exist = (
                        SELECT [Id] FROM [dbo].[Subjekty]
                        WHERE ([Ico] = @Ico) AND ([Deleted] IS NULL));

                    IF @Exist IS NOT NULL
                    BEGIN

                        -- UPDATE
                        UPDATE [dbo].[Subjekty] SET
                            [Updated] = CURRENT_TIMESTAMP,
                            [Ico] = @Ico,
                            [Dic] = @Dic,
                            [ObchJmeno] = @ObchJmeno,
                            [Stat] = @Stat,
                            [Kraj] = @Kraj,
                            [Okres] = @Okres,
                            [Obec] = @Obec,
                            [Obvod] = @Obvod,
                            [Ulice] = @Ulice,
                            [CisloDomovni] = @CisloDomovni,
                            [CisloOrientacni] = @CisloOrientacni,
                            [Psc] = @Psc,
                            [DorucovaciAdresa1] = @DorucovaciAdresa1,
                            [DorucovaciAdresa2] = @DorucovaciAdresa2,
                            [DorucovaciAdresa3] = @DorucovaciAdresa3,
                            [DatumVzniku] = @DatumVzniku,
                            [DatumZaniku] = @DatumZaniku,
                            [DatumAktualizace] = @DatumAktualizace,
                            [Description] = @Description
                        WHERE
                            [Id] = @Exist;

                    END ELSE BEGIN

                        -- INSERT
                        INSERT INTO [dbo].[Subjekty] (
                            [Created],
                            [Ico],
                            [Dic],
                            [ObchJmeno],
                            [Stat],
                            [Kraj],
                            [Okres],
                            [Obec],
                            [Obvod],
                            [Ulice],
                            [CisloDomovni],
                            [CisloOrientacni],
                            [Psc],
                            [DorucovaciAdresa1],
                            [DorucovaciAdresa2],
                            [DorucovaciAdresa3],
                            [DatumVzniku],
                            [DatumZaniku],
                            [DatumAktualizace],
                            [Description]
                        ) VALUES (
                            SYSDATETIME(),
                            @Ico,
                            @Dic,
                            @ObchJmeno,
                            @Stat,
                            @Kraj,
                            @Okres,
                            @Obec,
                            @Obvod,
                            @Ulice,
                            @CisloDomovni,
                            @CisloOrientacni,
                            @Psc,
                            @DorucovaciAdresa1,
                            @DorucovaciAdresa2,
                            @DorucovaciAdresa3,
                            @DatumVzniku,
                            @DatumZaniku,
                            @DatumAktualizace,
                            @Description);

                        SET @Exist = SCOPE_IDENTITY();
                    END

                    SELECT [Id], [Created], [Updated], [Deleted],
                        [Ico], [Dic], [ObchJmeno],
                        [Stat], [Kraj], [Okres], [Obec], [Obvod], [Ulice], [CisloDomovni], [CisloOrientacni], [Psc],
                        [DorucovaciAdresa1], [DorucovaciAdresa2], [DorucovaciAdresa3],
                        [DatumVzniku], [DatumZaniku], [DatumAktualizace],
                        [Description]
                    FROM [dbo].[Subjekty] WHERE ([Id] = @Exist);

                    RETURN 1;
                END
                """;

        return sp;
    }
}
