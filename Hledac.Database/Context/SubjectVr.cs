using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hledac.Database.Context;

[Index(nameof(ICO))]
public class SubjectVr
{
    [Key]
    public int Id { get; set; }

    [Required, MinLength(8), MaxLength(8)]
    public required string ICO { get; set; }

    public int? PravniForma { get; set; }

    public DateTime? DatumPosledniKontroly { get; set; }

    [MaxLength(10)]
    public string? Rejstrik { get; set; }

    public string? RequestError { get; set; }

    public string? ResponseError { get; set; }

    public DateTime? Aktualizace_DB { get; set; }

    [MaxLength(52)]
    public string? TypVypisu { get; set; }

    [MaxLength(20)]
    public string? S_StavSubjektu { get; set; }

    public int? S_Konkurz { get; set; }

    public int? S_Vyrovnani { get; set; }

    public int? S_Zamitnuti { get; set; }

    public int? S_Likvidace { get; set; }

    [Required, MinLength(1), MaxLength(2000)]
    public required string ObchodniFirma { get; set; }

    [MaxLength(50)]
    public string? Jmeno { get; set; }

    [MaxLength(50)]
    public string? Prijmeni { get; set; }

    public DateTime? DatumNarozeni { get; set; }

    public int? PF_Kody { get; set; }

    [MaxLength(100)]
    public string? PF_Nazev { get; set; }

    [MaxLength(50)]
    public string? PF_Osoba { get; set; }

    [MaxLength(50)]
    public string? PF_Text { get; set; }

    [MaxLength(50)]
    public string? A_IDAdresy { get; set; }

    [MaxLength(50)]
    public string? A_KodStatu { get; set; }

    [MaxLength(50)]
    public string? A_NazevStatu { get; set; }

    [MaxLength(50)]
    public string? A_NazevOkresu { get; set; }

    [MaxLength(50)]
    public string? A_NazevObce { get; set; }

    [MaxLength(50)]
    public string? A_NazevCastiObce { get; set; }

    [MaxLength(50)]
    public string? A_NazevUlice { get; set; }

    public int? A_CisloDomovni { get; set; }
    
    public int? A_TypCisloDomovni { get; set; }

    [MaxLength(10)]
    public string? A_CisloOrientacni { get; set; }

    [MaxLength(10)]
    public string? A_PSC { get; set; }

    public DateTime? DatumZapisu { get; set; }

    [MaxLength(50)]
    public string? MistoZapisu { get; set; }

    [MaxLength(10)]
    public string? ZnackaZapisu { get; set; }
}
