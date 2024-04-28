
namespace Hledac.Domain.Rss.Services;

public interface IRssRepositoryService
{
    Task<IEnumerable<Feed>> GetAllAsync();
    Task<Feed> GetByIdAsync(int id);
    Task AddAsync(RssSite rssSite, Feed feed);
    Task UpdateAsync(Feed feed);
    Task DeleteAsync(int id);
}
