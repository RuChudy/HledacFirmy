namespace HledacFirmy.Hledac;

/// <summary>
/// Vysledek vyhledani.
/// </summary>
public enum HledacVysledekEnum
{
    /// <summary>
    /// Chyba behem hledani.
    /// </summary>
    Chyba,

    /// <summary>
    /// IC nebylo nalezeno, neexistuje.
    /// </summary>
    Nenalezeno,

    /// <summary>
    /// IC nalezeno.
    /// </summary>
    Nalezeno
}
