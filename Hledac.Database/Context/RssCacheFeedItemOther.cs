using System.ComponentModel.DataAnnotations.Schema;

namespace Hledac.Database.Context;

/// <summary>
/// Seznam kategorií jako JSON objekt.
/// </summary>
[ComplexType]
public class RssCacheFeedItemOther
{
    public required List<string> Categories { get; set; }
}
