using HledacFirmy.Subjekty;

namespace HledacFirmy.Hledac;

/// <summary>
/// Vysledek vyhledani subjektu dle ICO.
/// </summary>
public class HledacVysledekDto
{
    /// <summary>
    /// Vysledek vyhledani.
    /// </summary>
    public HledacVysledekEnum Vysledek { get; set; }

    /// <summary>
    /// Podrobnosti subjektu.
    /// </summary>
    public SubjektDto? Subjekt { get; set; }
}
