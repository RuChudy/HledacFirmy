using System.ComponentModel.DataAnnotations;

namespace Hledac.Database.Context;

/// <summary>
/// Subjekt - firma, definice tabulky.
/// </summary>
[Index(nameof(Ico))]
public class Subjekt
{
    [Key]
    public int Id { get; set; }

    public DateTime Created { get; set; }
    public DateTime? Updated { get; set; }
    public DateTime? Deleted { get; set; }


    [Required, MinLength(8), MaxLength(8)]
    public required string Ico { get; set; }

    [Required, MinLength(1), MaxLength(2000)]
    public required string ObchJmeno { get; set; }

    /// <summary>CZNNNNNNNNNN</summary>
    [MaxLength(12)]
    public string? Dic { get; set; }

    public string? Stat { get; set; }
    public string? Kraj { get; set; }
    public string? Okres { get; set; }
    public string? Obec { get; set; }
    public string? Obvod { get; set; }
    public string? Ulice { get; set; }
    public string? CisloDomovni { get; set; }
    public string? CisloOrientacni { get; set; }
    public string? Psc { get; set; }
    public string? DorucovaciAdresa1 { get; set; }
    public string? DorucovaciAdresa2 { get; set; }
    public string? DorucovaciAdresa3 { get; set; }

    public DateTime DatumVzniku { get; set; }
    public DateTime? DatumZaniku { get; set; }
    public DateTime DatumAktualizace { get; set; }

    public string? Description { get; set; }
}
