namespace HledacFirmy.Ares.Services;

/// <summary>
/// Nastavení pro systém ARES.
/// </summary>
public class AresSettings
{
    public static string SectionName = "Ares";

    /// <summary>
    /// Hlavní, bázová URL adresa rest api ARES služby.
    /// </summary>
    public string? Uri { get; set; }
}
