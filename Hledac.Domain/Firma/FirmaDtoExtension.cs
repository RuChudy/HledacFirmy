using Hledac.Database.Context;
using Hledac.Domain.Ares;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Hledac.Domain.Firma;

/// <summary>
/// Rozšíení pro DTO objekt FirmaDto.
/// </summary>
internal static class FirmaDtoExtension
{
    public static FirmaDto ToFirmaDto(this Subjekt subjekt)
    {
        return new FirmaDto
        {
            Id = subjekt.Id,
            Created = subjekt.Created,
            Updated = subjekt.Updated,
            Deleted = subjekt.Deleted,
            Ico = subjekt.Ico,
            Dic = subjekt.Dic,
            Jmeno = subjekt.ObchJmeno,
            Stat = subjekt.Stat,
            Kraj = subjekt.Kraj,
            Okres = subjekt.Okres,
            Obec = subjekt.Obec,
            Obvod = subjekt.Obvod,
            Ulice = subjekt.Ulice,
            CisloDomovni = subjekt.CisloDomovni,
            CisloOrientacni = subjekt.CisloOrientacni,
            PscTxt = subjekt.Psc,
            DorucovaciAdresa1 = subjekt.DorucovaciAdresa1,
            DorucovaciAdresa2 = subjekt.DorucovaciAdresa2,
            DorucovaciAdresa3 = subjekt.DorucovaciAdresa3,
            DatumVzniku = subjekt.DatumVzniku,
            DatumZaniku = subjekt.DatumZaniku,
            DatumAktualizace = subjekt.DatumAktualizace,
            Description = subjekt.Description,
        };
    }

    public static FirmaReqestInfo ToFirmaReqest(this AresEkonomickySubjekt subject)
    {
        return new FirmaReqestInfo
        {
            OdpovedTyp = "",
            AktualizaceDB = DateTime.Now,
            DatumCasVypisu = DateTime.Now,
            TypVypisu = "",
            Ico = subject.Ico,
            ObchodniFirma = "",
            DatumZapisu = DateTime.Now,
            MistoZapisu = "",
            ZnackaZapisu = "",
            Jmeno = "",
            Prijmeni = "",
            DatumNarozeni = DateTime.Now,
            Error = ""

//     public FirmaRequestStav? Stav = "",
//    public FirmaRequestLegalForm? PravniForma { get; set; }
//    public FirmaRequestAddress? Adresa { get; set; }
        };
    }
}
