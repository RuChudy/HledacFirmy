namespace Hledac.Domain.Ares;

/// <summary>
/// Seznam registrací ekonomického subjektu v jednotlivých zdrojích.
/// </summary>
public sealed class AresSeznamRegistraci
{
    /// <summary>Stav ekonomického subjektu ve zdroji VR(Veřejné rejstříky) - kód(ciselnikKod: StavZdroje, zdroj: com)</summary>
    public string? stavZdrojeVr { get; set; }

    /// <summary>Stav ekonomického subjektu ve zdroji RES(Registr ekonomických subjektů) - kód(ciselnikKod: StavZdroje, zdroj: com)</summary>
    public string? stavZdrojeRes { get; set; }

    /// <summary>Stav ekonomického subjektu ve zdroji RŽP(Registr živnostenského podnikání) - kód(ciselnikKod: StavZdroje, zdroj: com)</summary>
    public string? stavZdrojeRzp { get; set; }

    /// <summary>Stav ekonomického subjektu ve zdroji NRPZS(Národní registr poskytovatelů zdrovotnických služeb) - kód(ciselnikKod: StavZdroje, zdroj: com)</summary>
    public string? stavZdrojeNrpzs { get; set; }

    /// <summary>Stav ekonomického subjektu ve zdroji RPSH(Registr politických stran a hnutí) - kód(ciselnikKod: StavZdroje, zdroj: com)</summary>
    public string? stavZdrojeRpsh { get; set; }

    /// <summary>Stav ekonomického subjektu ve zdroji RCNS(Registr církví a náboženských společenství) - kód(ciselnikKod: StavZdroje, zdroj: com)</summary>
    public string? stavZdrojeRcns { get; set; }

    /// <summary>Stav ekonomického subjektu ve zdroji SZR(Společný zemědělský registr) - kód(ciselnikKod: StavZdroje, zdroj: com)</summary>
    public string? stavZdrojeSzr { get; set; }

    /// <summary>Stav ekonomického subjektu ve zdroji DPH(Registr plátců daně s přidané hodnoty) - kód(ciselnikKod: StavZdroje, zdroj: com)</summary>
    public string? stavZdrojeDph { get; set; }

    /// <summary>Stav ekonomického subjektu ve zdroji SD(Registr plátců spotřební daně) - kód(ciselnikKod: StavZdroje, zdroj: com)</summary>
    public string? stavZdrojeSd { get; set; }

    /// <summary>Stav ekonomického subjektu ve zdroji ISIR(Insolvenční rejstřík) - kód(ciselnikKod: StavZdroje, zdroj: com)</summary>
    public string? stavZdrojeIr { get; set; }

    /// <summary>Stav ekonomického subjektu ve zdroji CEÚ(Centrální evidence úpadců) - kód(ciselnikKod: StavZdroje, zdroj: com)</summary>
    public string? stavZdrojeCeu { get; set; }

    /// <summary>Stav ekonomického subjektu ve zdroji RŠ(Registr škol) - kód(ciselnikKod: StavZdroje, zdroj: com)</summary>
    public string? stavZdrojeRs { get; set; }

    /// <summary>Stav ekonomického subjektu ve zdroji RED(Registr evidence dotací) - kód(ciselnikKod: StavZdroje, zdroj: com)}</summary>
    public string? stavZdrojeRed { get; set; }
}
