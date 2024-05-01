using System.ComponentModel.DataAnnotations;

namespace Hledac.Database.Context;

/// <summary>
/// RssCacheFeed - RSS kanál, definice tabulky.
/// </summary>
[Index(nameof(SiteUri), IsUnique = true)]
public class RssCacheFeed
{
    [Key]
    public int Id { get; set; }

    public DateTime Created { get; set; }
    public DateTime? Updated { get; set; }
    public DateTime? Deleted { get; set; }


    [Required, MinLength(1), MaxLength(900)]
    public required string SiteUri { get; set; }

    public string? Link { get; set; }
    public string? Title { get; set; }
    public string? Language { get; set; }
    public string? Copyright { get; set; }
    public string? Description { get; set; }

    /// <summary>Zprávy</summary>
    public List<RssCacheFeedItem> FeedItems { get; } = new List<RssCacheFeedItem>()!;
}
