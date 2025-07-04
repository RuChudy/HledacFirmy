using System;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Entities.Auditing;

namespace HledacFirmy.Entities;

/// <summary>
/// Subjekt - firma, definice tabulky.
/// </summary>
public class Subjekt : FullAuditedAggregateRoot<Guid>
{
    /// <summary>Identifikační číslo osoby - IČO</summary>
    public required string Ico { get; set; }

    /// <summary>Obchodní jméno ekonomického subjektu</summary>
    public required string ObchJmeno { get; set; }

    /// <summary>Daňové identifikační číslo skupiny plátce DPH ve formátu CZNNNNNNNNNN</summary>
    public string? Dic { get; set; }

    /// <summary>Název státu</summary>
    public string? Stat { get; set; }

    /// <summary>Název kraje</summary>
    public string? Kraj { get; set; }

    /// <summary>Název okresu</summary>
    public string? Okres { get; set; }

    /// <summary>Název obce</summary>
    public string? Obec { get; set; }

    /// <summary>Název správního obvodu</summary>
    public string? Obvod { get; set; }

    /// <summary>Název ulice, veřejného prostranství</summary>
    public string? Ulice { get; set; }

    /// <summary>Číslo domovní</summary>
    public string? CisloDomovni { get; set; }

    /// <summary>Číslo orientační - číselná část</summary>
    public string? CisloOrientacni { get; set; }

    /// <summary>Poštovní směrovací číslo adresní pošty</summary>
    public string? Psc { get; set; }

    /// <summary>Dorucovaci adresa</summary>
    public string? DorucovaciAdresa1 { get; set; }

    /// <summary>Dorucovaci adresa</summary>
    public string? DorucovaciAdresa2 { get; set; }

    /// <summary>Dorucovaci adresa</summary>
    public string? DorucovaciAdresa3 { get; set; }

    /// <summary>Datum vzniku ekonomického subjektu</summary>
    public DateTime DatumVzniku { get; set; }

    /// <summary>Datum zániku ekonomického subjektu</summary>
    public DateTime? DatumZaniku { get; set; }

    /// <summary>Datum aktualizace záznamu</summary>
    public DateTime DatumAktualizace { get; set; }

    /// <summary>Interni popis</summary>
    public string? Description { get; set; }
}
