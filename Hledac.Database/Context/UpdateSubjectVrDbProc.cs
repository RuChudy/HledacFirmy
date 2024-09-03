using System.Data;
using Azure.Core;
using Microsoft.Data.SqlClient;

namespace Hledac.Database.Context;

public partial class SubjektDbContext
{
    /// <summary>
    /// Vytvoří nebo aktualizuje subjekt přez proceduru Subjekt_Save.
    /// </summary>
    /// <returns>Záznam z tabulky Subjekt</returns>
    public SubjectVr? UpdateSubjektVr(SubjectVr subject)
    {
        ArgumentNullException.ThrowIfNull(subject);

        SubjectVr? result = SubjectVrSet
            .FromSqlRaw("""
                EXECUTE [dbo].[UpdateSubject] @ICO, @PravniForma, @DatumPosledniKontroly,
                @Rejstrik, @RequestError, @ResponseError, @Aktualizace_DB, @TypVypisu,
                @S_StavSubjektu, @S_Konkurz, @S_Vyrovnani, @S_Zamitnuti, @S_Likvidace,
                @ObchodniFirma, @Jmeno, @Prijmeni, @DatumNarozeni,
                @PF_Kody, @A_IDAdresy, @A_KodStatu, @A_NazevStatu, @A_NazevOkresu, @A_NazevObce, @A_NazevCastiObce,
                @A_NazevUlice, @A_CisloDomovni, @A_TypCisloDomovni, @A_CisloOrientacni, @A_PSC,
                @DatumZapisu, @MistoZapisu, @ZnackaZapisu
                """,
                new SqlParameter("@ICO", SqlDbType.NVarChar, 8) { Value = subject.ICO ?? (object)DBNull.Value },
                new SqlParameter("@PravniForma", SqlDbType.Int) { Value = subject.PravniForma ?? (object)DBNull.Value },
                new SqlParameter("@DatumPosledniKontroly", SqlDbType.DateTime) { Value = subject.DatumPosledniKontroly ?? (object)DBNull.Value },
                new SqlParameter("@Rejstrik", SqlDbType.NVarChar, 10) { Value = subject.Rejstrik ?? (object)DBNull.Value },
                new SqlParameter("@RequestError", SqlDbType.NVarChar, -1) { Value = subject.RequestError ?? (object)DBNull.Value },
                new SqlParameter("@ResponseError", SqlDbType.NVarChar, -1) { Value = subject.ResponseError ?? (object)DBNull.Value },
                new SqlParameter("@Aktualizace_DB", SqlDbType.DateTime) { Value = subject.Aktualizace_DB ?? (object)DBNull.Value },
                new SqlParameter("@TypVypisu", SqlDbType.NVarChar, 52) { Value = subject.TypVypisu ?? (object)DBNull.Value },
                new SqlParameter("@S_StavSubjektu", SqlDbType.NVarChar, 20) { Value = subject.S_StavSubjektu ?? (object)DBNull.Value },
                new SqlParameter("@S_Konkurz", SqlDbType.Int) { Value = subject.S_Konkurz ?? (object)DBNull.Value },
                new SqlParameter("@S_Vyrovnani", SqlDbType.Int) { Value = subject.S_Vyrovnani ?? (object)DBNull.Value },
                new SqlParameter("@S_Zamitnuti", SqlDbType.Int) { Value = subject.S_Zamitnuti ?? (object)DBNull.Value },
                new SqlParameter("@S_Likvidace", SqlDbType.Int) { Value = subject.S_Likvidace ?? (object)DBNull.Value },
                new SqlParameter("@ObchodniFirma", SqlDbType.NVarChar, 2000) { Value = subject.ObchodniFirma ?? (object)DBNull.Value },
                new SqlParameter("@Jmeno", SqlDbType.NVarChar, 50) { Value = subject.Jmeno ?? (object)DBNull.Value },
                new SqlParameter("@Prijmeni", SqlDbType.NVarChar, 50) { Value = subject.Prijmeni ?? (object)DBNull.Value },
                new SqlParameter("@DatumNarozeni", SqlDbType.DateTime) { Value = subject.DatumNarozeni ?? (object)DBNull.Value },
                new SqlParameter("@PF_Kody", SqlDbType.Int) { Value = subject.PF_Kody ?? (object)DBNull.Value },
                new SqlParameter("@PF_Nazev", SqlDbType.NVarChar, 100) { Value = subject.PF_Nazev ?? (object)DBNull.Value },
                new SqlParameter("@PF_Osoba", SqlDbType.NVarChar, 50) { Value = subject.PF_Osoba ?? (object)DBNull.Value },
                new SqlParameter("@PF_Text", SqlDbType.NVarChar, 50) { Value = subject.PF_Text ?? (object)DBNull.Value },
                new SqlParameter("@A_IDAdresy", SqlDbType.NVarChar, 50) { Value = subject.A_IDAdresy ?? (object)DBNull.Value },
                new SqlParameter("@A_KodStatu", SqlDbType.NVarChar, 50) { Value = subject.A_KodStatu ?? (object)DBNull.Value },
                new SqlParameter("@A_NazevStatu", SqlDbType.NVarChar, 50) { Value = subject.A_NazevStatu ?? (object)DBNull.Value },
                new SqlParameter("@A_NazevOkresu", SqlDbType.NVarChar, 50) { Value = subject.A_NazevOkresu ?? (object)DBNull.Value },
                new SqlParameter("@A_NazevObce", SqlDbType.NVarChar, 50) { Value = subject.A_NazevObce ?? (object)DBNull.Value },
                new SqlParameter("@A_NazevCastiObce", SqlDbType.NVarChar, 50) { Value = subject.A_NazevCastiObce ?? (object)DBNull.Value },
                new SqlParameter("@A_NazevUlice", SqlDbType.NVarChar, 50) { Value = subject.A_NazevUlice ?? (object)DBNull.Value },
                new SqlParameter("@A_CisloDomovni", SqlDbType.Int) { Value = subject.A_CisloDomovni ?? (object)DBNull.Value },
                new SqlParameter("@A_TypCisloDomovni", SqlDbType.Int) { Value = subject.A_TypCisloDomovni ?? (object)DBNull.Value },
                new SqlParameter("@A_CisloOrientacni", SqlDbType.NVarChar, 10) { Value = subject.A_CisloOrientacni ?? (object)DBNull.Value },
                new SqlParameter("@A_PSC", SqlDbType.NVarChar, 10) { Value = subject.A_PSC ?? (object)DBNull.Value },
                new SqlParameter("@DatumZapisu", SqlDbType.DateTime) { Value = subject.DatumZapisu ?? (object)DBNull.Value },
                new SqlParameter("@MistoZapisu", SqlDbType.NVarChar, 50) { Value = subject.MistoZapisu?? (object)DBNull.Value },
                new SqlParameter("@ZnackaZapisu", SqlDbType.NVarChar, 10) { Value = subject.ZnackaZapisu ?? (object)DBNull.Value }
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
    public static string CreateSqlProc_UbpdateSubjekt_V1()
    {
        // migrationBuilder.Sql(SubjektDbContext.CreateSqlProc_UbpdateSubjekt_V1());

        var sp = """
                CREATE PROCEDURE [dbo].[UpdateSubject]
                    @ICO nvarchar(8),
                    @PravniForma int,
                    @DatumPosledniKontroly datetime2(7),
                    @Rejstrik nvarchar(10),
                    @RequestError nvarchar(max),
                    @ResponseError nvarchar(max),
                    @Aktualizace_DB datetime2(7),
                    @TypVypisu nvarchar(52),
                    @S_StavSubjektu nvarchar(20),
                    @S_Konkurz int,
                    @S_Vyrovnani int,
                    @S_Zamitnuti int,
                    @S_Likvidace int,
                    @ObchodniFirma nvarchar(2000),
                    @Jmeno nvarchar(50),
                    @Prijmeni nvarchar(50),
                    @DatumNarozeni datetime2(7),
                    @PF_Kody int,
                    @PF_Nazev nvarchar(100),
                    @PF_Osoba nvarchar(50),
                    @PF_Text nvarchar(50),
                    @A_IDAdresy nvarchar(50),
                    @A_KodStatu nvarchar(50),
                    @A_NazevStatu nvarchar(50),
                    @A_NazevOkresu nvarchar(50),
                    @A_NazevObce nvarchar(50),
                    @A_NazevCastiObce nvarchar(50),
                    @A_NazevUlice nvarchar(50),
                    @A_CisloDomovni int,
                    @A_TypCisloDomovni int,
                    @A_CisloOrientacni nvarchar(10),
                    @A_PSC nvarchar(10),
                    @DatumZapisu datetime2(7),
                    @MistoZapisu nvarchar(50),
                    @ZnackaZapisu nvarchar(10)
                AS
                BEGIN
                    SET NOCOUNT ON;
                    DECLARE @Exist int;

                    SET @Exist = (SELECT [Id] FROM [dbo].[SubjectVrSet] WHERE [ICO] = @ICO);

                    IF @Exist IS NOT NULL
                    BEGIN

                        -- UPDATE
                        UPDATE [dbo].[SubjectVrSet] SET
                            [PravniForma] = @PravniForma,
                            [DatumPosledniKontroly] = @DatumPosledniKontroly,
                            [Rejstrik] = @Rejstrik,
                            [RequestError] = @RequestError,
                            [ResponseError] = @ResponseError,
                            [Aktualizace_DB] = SYSDATETIME(),
                            [TypVypisu] = @TypVypisu,
                            [S_StavSubjektu] = @S_StavSubjektu,
                            [S_Konkurz] = @S_Konkurz,
                            [S_Vyrovnani] = @S_Vyrovnani,
                            [S_Zamitnuti] = @S_Zamitnuti,
                            [S_Likvidace] = @S_Likvidace,
                            [ObchodniFirma] = @ObchodniFirma,
                            [Jmeno] = @Jmeno,
                            [Prijmeni] = @Prijmeni,
                            [DatumNarozeni] = @DatumNarozeni,
                            [PF_Kody] = @PF_Kody,
                            [PF_Nazev] = @PF_Nazev,
                            [PF_Osoba] = @PF_Osoba,
                            [PF_Text] = @PF_Text,
                            [A_IDAdresy] = @A_IDAdresy,
                            [A_KodStatu] = @A_KodStatu,
                            [A_NazevStatu] = @A_NazevStatu,
                            [A_NazevOkresu] = @A_NazevOkresu,
                            [A_NazevObce] = @A_NazevObce,
                            [A_NazevCastiObce] = @A_NazevCastiObce,
                            [A_NazevUlice] = @A_NazevUlice,
                            [A_CisloDomovni] = @A_CisloDomovni,
                            [A_TypCisloDomovni] = @A_TypCisloDomovni,
                            [A_CisloOrientacni] = @A_CisloOrientacni,
                            [A_PSC] = @A_PSC,
                            [DatumZapisu] = @DatumZapisu,
                            [MistoZapisu] = @MistoZapisu,
                            [ZnackaZapisu] = @ZnackaZapisu
                        WHERE
                            [Id] = @Exist;

                    END ELSE BEGIN

                        -- INSERT
                        INSERT INTO [dbo].[SubjectVrSet] (
                            [ICO], [PravniForma], [DatumPosledniKontroly], [Rejstrik], [RequestError],
                            [ResponseError], [Aktualizace_DB], [TypVypisu], [S_StavSubjektu], [S_Konkurz],
                            [S_Vyrovnani], [S_Zamitnuti], [S_Likvidace], [ObchodniFirma], [Jmeno], [Prijmeni], [DatumNarozeni],
                            [PF_Kody], [PF_Nazev], [PF_Osoba], [PF_Text],
                            [A_IDAdresy], [A_KodStatu], [A_NazevStatu], [A_NazevOkresu], [A_NazevObce], [A_NazevCastiObce],
                            [A_NazevUlice], [A_CisloDomovni], [A_TypCisloDomovni], [A_CisloOrientacni], [A_PSC],
                            [DatumZapisu], [MistoZapisu], [ZnackaZapisu]
                        ) VALUES (
                            @ICO, @PravniForma, @DatumPosledniKontroly, @Rejstrik, @RequestError,
                            @ResponseError, SYSDATETIME(), @TypVypisu, @S_StavSubjektu, @S_Konkurz,
                            @S_Vyrovnani, @S_Zamitnuti, @S_Likvidace, @ObchodniFirma, @Jmeno, @Prijmeni, @DatumNarozeni,
                            @PF_Kody, @PF_Nazev, @PF_Osoba, @PF_Text,
                            @A_IDAdresy, @A_KodStatu, @A_NazevStatu, @A_NazevOkresu, @A_NazevObce, @A_NazevCastiObce,
                            @A_NazevUlice, @A_CisloDomovni, @A_TypCisloDomovni, @A_CisloOrientacni, @A_PSC,
                            @DatumZapisu, @MistoZapisu, @ZnackaZapisu
                        );

                        SET @Exist = SCOPE_IDENTITY();
                    END

                    SELECT [Id], [ICO], [PravniForma], [DatumPosledniKontroly], [Rejstrik], [RequestError],
                        [ResponseError], [Aktualizace_DB], [TypVypisu], [S_StavSubjektu], [S_Konkurz],
                        [S_Vyrovnani], [S_Zamitnuti], [S_Likvidace], [ObchodniFirma], [Jmeno], [Prijmeni], [DatumNarozeni],
                        [PF_Kody], [PF_Nazev], [PF_Osoba], [PF_Text],
                        [A_IDAdresy], [A_KodStatu], [A_NazevStatu], [A_NazevOkresu], [A_NazevObce], [A_NazevCastiObce],
                        [A_NazevUlice], [A_CisloDomovni], [A_TypCisloDomovni], [A_CisloOrientacni], [A_PSC],
                        [DatumZapisu], [MistoZapisu], [ZnackaZapisu]
                    FROM [dbo].[SubjectVrSet] WHERE ([Id] = @Exist);

                    RETURN 1;
                    SET NOCOUNT ON;
                    DECLARE @Exist int;

                    SET @Exist = (SELECT [Id] FROM [dbo].[SubjectVrSet] WHERE [ICO] = @ICO);

                    IF @Exist IS NOT NULL
                    BEGIN

                        -- UPDATE
                        UPDATE [dbo].[SubjectVrSet] SET
                            [PravniForma] = @PravniForma,
                            [DatumPosledniKontroly] = @DatumPosledniKontroly,
                            [Rejstrik] = @Rejstrik,
                            [RequestError] = @RequestError,
                            [ResponseError] = @ResponseError,
                            [Aktualizace_DB] = SYSDATETIME(),
                            [TypVypisu] = @TypVypisu,
                            [S_StavSubjektu] = @S_StavSubjektu,
                            [S_Konkurz] = @S_Konkurz,
                            [S_Vyrovnani] = @S_Vyrovnani,
                            [S_Zamitnuti] = @S_Zamitnuti,
                            [S_Likvidace] = @S_Likvidace,
                            [ObchodniFirma] = @ObchodniFirma,
                            [Jmeno] = @Jmeno,
                            [Prijmeni] = @Prijmeni,
                            [DatumNarozeni] = @DatumNarozeni,
                            [PF_Kody] = @PF_Kody,
                            [PF_Nazev] = @PF_Nazev,
                            [PF_Osoba] = @PF_Osoba,
                            [PF_Text] = @PF_Text,
                            [A_IDAdresy] = @A_IDAdresy,
                            [A_KodStatu] = @A_KodStatu,
                            [A_NazevStatu] = @A_NazevStatu,
                            [A_NazevOkresu] = @A_NazevOkresu,
                            [A_NazevObce] = @A_NazevObce,
                            [A_NazevCastiObce] = @A_NazevCastiObce,
                            [A_NazevUlice] = @A_NazevUlice,
                            [A_CisloDomovni] = @A_CisloDomovni,
                            [A_TypCisloDomovni] = @A_TypCisloDomovni,
                            [A_CisloOrientacni] = @A_CisloOrientacni,
                            [A_PSC] = @A_PSC,
                            [DatumZapisu] = @DatumZapisu,
                            [MistoZapisu] = @MistoZapisu,
                            [ZnackaZapisu] = @ZnackaZapisu
                        WHERE
                            [Id] = @Exist;

                    END ELSE BEGIN

                        -- INSERT
                        INSERT INTO [dbo].[SubjectVrSet] (
                            [ICO], [PravniForma], [DatumPosledniKontroly], [Rejstrik], [RequestError],
                            [ResponseError], [Aktualizace_DB], [TypVypisu], [S_StavSubjektu], [S_Konkurz],
                            [S_Vyrovnani], [S_Zamitnuti], [S_Likvidace], [ObchodniFirma], [Jmeno], [Prijmeni], [DatumNarozeni],
                            [PF_Kody], [PF_Nazev], [PF_Osoba], [PF_Text],
                            [A_IDAdresy], [A_KodStatu], [A_NazevStatu], [A_NazevOkresu], [A_NazevObce], [A_NazevCastiObce],
                            [A_NazevUlice], [A_CisloDomovni], [A_TypCisloDomovni], [A_CisloOrientacni], [A_PSC],
                            [DatumZapisu], [MistoZapisu], [ZnackaZapisu]
                        ) VALUES (
                            @ICO, @PravniForma, @DatumPosledniKontroly, @Rejstrik, @RequestError,
                            @ResponseError, SYSDATETIME(), @TypVypisu, @S_StavSubjektu, @S_Konkurz,
                            @S_Vyrovnani, @S_Zamitnuti, @S_Likvidace, @ObchodniFirma, @Jmeno, @Prijmeni, @DatumNarozeni,
                            @PF_Kody, @PF_Nazev, @PF_Osoba, @PF_Text,
                            @A_IDAdresy, @A_KodStatu, @A_NazevStatu, @A_NazevOkresu, @A_NazevObce, @A_NazevCastiObce,
                            @A_NazevUlice, @A_CisloDomovni, @A_TypCisloDomovni, @A_CisloOrientacni, @A_PSC,
                            @DatumZapisu, @MistoZapisu, @ZnackaZapisu
                        );

                        SET @Exist = SCOPE_IDENTITY();
                    END

                    SELECT [Id], [ICO], [PravniForma], [DatumPosledniKontroly], [Rejstrik], [RequestError],
                        [ResponseError], [Aktualizace_DB], [TypVypisu], [S_StavSubjektu], [S_Konkurz],
                        [S_Vyrovnani], [S_Zamitnuti], [S_Likvidace], [ObchodniFirma], [Jmeno], [Prijmeni], [DatumNarozeni],
                        [PF_Kody], [PF_Nazev], [PF_Osoba], [PF_Text],
                        [A_IDAdresy], [A_KodStatu], [A_NazevStatu], [A_NazevOkresu], [A_NazevObce], [A_NazevCastiObce],
                        [A_NazevUlice], [A_CisloDomovni], [A_TypCisloDomovni], [A_CisloOrientacni], [A_PSC],
                        [DatumZapisu], [MistoZapisu], [ZnackaZapisu]
                    FROM [dbo].[SubjectVrSet] WHERE ([Id] = @Exist);

                    RETURN 1;
                END
                """;

        return sp;
    }
}
