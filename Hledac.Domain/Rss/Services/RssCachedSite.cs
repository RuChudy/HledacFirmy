namespace Hledac.Domain.Rss.Services;

public class RssCachedSite
{
    /// <summary>Id webu v databázi.</summary>
    public int Id { get; set; }

    /// <summary>Název webu</summary>
    public string Title { get; set; } = string.Empty;

    /// <summary>Odkaz na web s rss kanálem.</summary>
    public RssSiteUri Site { get; set; } = default!;
}
