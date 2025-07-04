using System;
using Volo.Abp.Application.Dtos;

namespace HledacFirmy.Subjekty;

[Serializable]
public class SubjektDto : FullAuditedEntityDto<Guid>
{
    public string Ico { get; set; } = string.Empty;
    public string ObchJmeno { get; set; } = string.Empty;
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
