
namespace Hledac.Domain.Rss.Services;

public interface IRssRepositoryService
{
    /// <summary>
    /// Seznam nesmazaných Rss webů v databázi.
    /// </summary>
    /// <param name="position">Od které pozice zero-based.</param>
    /// <param name="rowsCount">Kolik záznamů.</param>
    /// <returns>Seznam Rss webů v databázi.</returns>
    Task<ICollection<RssSite>> GetAllAsync(int position, int rows);

    /// <summary>
    /// Klíč nesmazaného Rss webu v datbázi.
    /// </summary>
    /// <param name="rssSite"></param>
    /// <returns>Klíč Rss webu v datbázi nebo null.</returns>
    Task<int?> GetSiteIdAsync(RssSite rssSite);

    /// <summary>
    /// Načte Rss kanál uložený v db dle Id.
    /// </summary>
    /// <param name="id">Id rss kanálu.</param>
    /// <returns>Rss kanál nebo null.</returns>
    Task<Feed?> GetByIdAsync(int id);

    /// <summary>
    /// Aktualizuje nebo vytvoří Rss Web kanálem.
    /// </summary>
    /// <param name="rssSite">Rss Web</param>
    /// <param name="feed">Rss kanál s článnky.</param>
    /// <returns>Počet aktualizovaných záznamů.</returns>
    Task<int> AddOrUpdateAsync(RssSite rssSite, Feed feed);

    /// <summary>
    /// Označí rss kanál jako smazaný.
    /// </summary>
    /// <param name="id">Id rss kanálu.</param>
    /// <returns>Došlo ke smazaní záznamu?</returns>
    Task<bool> DeleteAsync(int id);

    /// <summary>
    /// Odstraní nenávratně záznam rss kanálu.
    /// </summary>
    /// <param name="id">Id rss kanálu.</param>
    /// <returns>Počet smazaných záznamů.</returns>
    Task<int> RemoveAsync(int id);
}
