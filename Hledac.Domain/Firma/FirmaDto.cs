
namespace Hledac.Domain.Firma;

/// <summary>
/// Pohled na firmu.
/// </summary>
public sealed class FirmaDto
{
    public int Id { get; set; }
    public DateTime Created { get; set; }
    public DateTime? Updated { get; set; }
    public DateTime? Deleted { get; set; }

    public string? Ico { get; set; }
    public string? Dic { get; set; }
    public string? Jmeno { get; set; }

    public string? Stat { get; set; }
    public string? Kraj { get; set; }
    public string? Okres { get; set; }
    public string? Obec { get; set; }
    public string? Obvod { get; set; }
    public string? Ulice { get; set; }
    public string? CisloDomovni { get; set; }
    public string? CisloOrientacni { get; set; }
    public string? PscTxt { get; set; }

    public string? DorucovaciAdresa1 { get; set; }
    public string? DorucovaciAdresa2 { get; set; }
    public string? DorucovaciAdresa3 { get; set; }

    public DateTime DatumVzniku { get; set; }
    public DateTime? DatumZaniku { get; set; }
    public DateTime DatumAktualizace { get; set; }

    public string? Description { get; set; }


    public bool JePlatceDph => Dic?.Length > 0;
    public DateTime DatumPorizeni => Updated ?? Created;
}
