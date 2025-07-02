using System;

namespace HledacFirmy.Ares;

/// <summary>
/// Rozšíení pro objekty v AresEkonomickySubjektExtension.
/// </summary>
public static class AresEkonomickySubjektExtension
{
    public static bool InVr(this AresSeznamRegistraci subjekt) => "AKTIVNI".Equals(subjekt?.StavZdrojeVr, StringComparison.InvariantCultureIgnoreCase);
    public static bool InRes(this AresSeznamRegistraci subjekt) => "AKTIVNI".Equals(subjekt?.StavZdrojeRes, StringComparison.InvariantCultureIgnoreCase);
    public static bool InRzp(this AresSeznamRegistraci subjekt) => "AKTIVNI".Equals(subjekt?.StavZdrojeRzp, StringComparison.InvariantCultureIgnoreCase);
    public static bool InNrpzs(this AresSeznamRegistraci subjekt) => "AKTIVNI".Equals(subjekt?.StavZdrojeNrpzs, StringComparison.InvariantCultureIgnoreCase);
    public static bool InRpsh(this AresSeznamRegistraci subjekt) => "AKTIVNI".Equals(subjekt?.StavZdrojeRpsh, StringComparison.InvariantCultureIgnoreCase);
    public static bool InSzr(this AresSeznamRegistraci subjekt) => "AKTIVNI".Equals(subjekt?.StavZdrojeSzr, StringComparison.InvariantCultureIgnoreCase);
    public static bool InDph(this AresSeznamRegistraci subjekt) => "AKTIVNI".Equals(subjekt?.StavZdrojeDph, StringComparison.InvariantCultureIgnoreCase);
    public static bool InSd(this AresSeznamRegistraci subjekt) => "AKTIVNI".Equals(subjekt?.StavZdrojeSd, StringComparison.InvariantCultureIgnoreCase);
    public static bool InIr(this AresSeznamRegistraci subjekt) => "AKTIVNI".Equals(subjekt?.StavZdrojeIr, StringComparison.InvariantCultureIgnoreCase);
    public static bool InCeu(this AresSeznamRegistraci subjekt) => "AKTIVNI".Equals(subjekt?.StavZdrojeCeu, StringComparison.InvariantCultureIgnoreCase);
    public static bool InRs(this AresSeznamRegistraci subjekt) => "AKTIVNI".Equals(subjekt?.StavZdrojeRs, StringComparison.InvariantCultureIgnoreCase);
    public static bool InRed(this AresSeznamRegistraci subjekt) => "AKTIVNI".Equals(subjekt?.StavZdrojeRed, StringComparison.InvariantCultureIgnoreCase);

    // HISTORICKY, NEEXISTUJICI
}
