using System.ComponentModel.DataAnnotations;

namespace Hledac.Database.Context;

/// <summary>
/// RssCacheFeedItem - RSS článek v kanálu, definice tabulky.
/// </summary>
public class RssCacheFeedItem
{
    [Key]
    public int Id { get; set; }
    public required int FeedId { get; set; }

    public DateTime Created { get; set; }
    public DateTime? Updated { get; set; }
    public DateTime? Deleted { get; set; }

    public DateTime? PublishDate { get; set; }
    public string? Guid { get; set; }
    public string? Link { get; set; }
    public string? Title { get; set; }
    public string? Body { get; set; }

    /// <summary>Kategorie, seznam.</summary>
    public required RssCacheCategories Categories { get; set; }

    /// <summary>Feed kanál.</summary>
    public required RssCacheFeed Feed { get; set; }
}
