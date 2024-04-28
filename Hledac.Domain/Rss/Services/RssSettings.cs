namespace Hledac.Domain.Rss.Services;

public class RssSettings
{
    public static readonly string SectionName = "RssSettings";

    public string? Mail { get; set; }
    public string? DisplayName { get; set; }
    public string? Password { get; set; }
    public string? Host { get; set; }
    public int? Port { get; set; }
}
