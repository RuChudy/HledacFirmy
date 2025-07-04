using Volo.Abp.Identity;

namespace HledacFirmy;

public static class HledacFirmyConsts
{
    public const string DbTablePrefix = "App";
    public const string? DbSchema = null;
    public const string AdminEmailDefaultValue = IdentityDataSeedContributor.AdminEmailDefaultValue;
    public const string AdminPasswordDefaultValue = IdentityDataSeedContributor.AdminPasswordDefaultValue;

    public const long IcoMin = 0L;
    public const long IcoMax = 99999999L;
    public const int IcoLength = 8;

    public const int ObchJmenoLength = 255;
    public const int DicLength = 10;
    public const int PscLength = 5;
    public const int BasicTextLength = 255;
}
