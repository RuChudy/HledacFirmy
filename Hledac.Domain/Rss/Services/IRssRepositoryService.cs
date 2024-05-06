namespace Hledac.Domain.Rss.Services;

public interface IRssRepositoryService
{
    /// <summary>
    /// Seznam nesmazaných Rss webů v databázi.
    /// </summary>
    /// <param name="position">Od které pozice zero-based.</param>
    /// <param name="rowsCount">Kolik záznamů.</param>
    /// <param name="cancellation">Zastavení.</param>
    /// <returns>Seznam Rss webů v databázi.</returns>
    Task<ICollection<RssCachedSite>> GetAllSitesAsync(int position, int rows, CancellationToken cancellation = default);

    /// <summary>
    /// Klíč nesmazaného Rss webu v databázi.
    /// </summary>
    /// <param name="rssSite"></param>
    /// <param name="cancellation">Zastavení.</param>
    /// <returns>Klíč Rss webu v datbázi nebo null.</returns>
    Task<RssCachedSite?> GetSiteAsync(RssSiteUri rssSite, CancellationToken cancellation = default);

    /// <summary>
    /// Nesmazaný klíč Rss webu v databázi podle Id.
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cancellation">Zastavení.</param>
    /// <returns>Klíč Rss webu v datbázi nebo null.</returns>
    Task<RssCachedSite?> GetSiteByIdAsync(int id, CancellationToken cancellation = default);

    /// <summary>
    /// Načte Rss kanál uložený v db dle Id.
    /// </summary>
    /// <param name="id">Id rss kanálu.</param>
    /// <param name="cancellation">Zastavení.</param>
    /// <returns>Rss kanál nebo null.</returns>
    Task<Feed?> GetFeedByIdAsync(int id, CancellationToken cancellation = default);

    /// <summary>
    /// Aktualizuje nebo vytvoří Rss Web kanálem.
    /// </summary>
    /// <param name="rssSite">Rss Web</param>
    /// <param name="feed">Rss kanál s článnky.</param>
    /// <param name="cancellation">Zastavení.</param>
    /// <returns>Počet aktualizovaných záznamů.</returns>
    Task<int> AddOrUpdateAsync(RssSiteUri rssSite, Feed feed, CancellationToken cancellation = default);

    /// <summary>
    /// Označí rss kanál jako smazaný.
    /// </summary>
    /// <param name="id">Id rss kanálu.</param>
    /// <param name="cancellation">Zastavení.</param>
    /// <returns>Došlo ke smazaní záznamu?</returns>
    Task<bool> DeleteAsync(int id, CancellationToken cancellation = default);

    /// <summary>
    /// Označí několik rss kanálů jako smazaný.
    /// </summary>
    /// <param name="keys">Seznam Id rss kanálů.</param>
    /// <param name="cancellation">Zastavení.</param>
    /// <returns>Počet smazaných záznamů.</returns>
    Task<int> BulkDeleteAsync(IEnumerable<int> keys, CancellationToken cancellation = default);

    /// <summary>
    /// Odstraní nenávratně záznam rss kanálu.
    /// </summary>
    /// <param name="id">Id rss kanálu.</param>
    /// <param name="cancellation">Zastavení.</param>
    /// <returns>Počet smazaných záznamů.</returns>
    Task<int> RemoveAsync(int id, CancellationToken cancellation = default);

    /// <summary>
    /// Odstraní nenávratně rss kanály, které jsou označené jako smazané.
    /// </summary>
    /// <param name="cancellation">Zastavení.</param>
    /// <returns>Počet smazaných záznamů.</returns>
    Task<int> BatchRemoveDeletedAsync(CancellationToken cancellation = default);
}
