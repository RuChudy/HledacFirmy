namespace Hledac.Domain.Firma;

public class FirmaReqestInfo
{
    public string? OdpovedTyp { get; set; }
    public DateTime? AktualizaceDB { get; set; }
    public DateTime? DatumCasVypisu { get; set; }
    public string? TypVypisu { get; set; }
    public FirmaRequestStav? Stav { get; set; }
    public string? Ico { get; set; }
    public string? ObchodniFirma { get; set; }
    public FirmaRequestLegalForm? PravniForma { get; set; }
    public FirmaRequestAddress? Adresa { get; set; }
    public DateTime? DatumZapisu { get; set; }
    public string? MistoZapisu { get; set; }
    public string? ZnackaZapisu { get; set; }
    public string? Jmeno { get; set; }
    public string? Prijmeni { get; set; }
    public DateTime? DatumNarozeni { get; set; }
    public string? Error { get; set; }
}
