using HledacFirmy.Subjekty;

namespace HledacFirmy.Hledac;

public class HledacVysledekDto
{
    public HledacVysledekEnum Vysledek { get; set; }
    public SubjektDto? Subjekt { get; set; }
}
