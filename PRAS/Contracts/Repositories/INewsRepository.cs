using PRAS.Models;
using PRAS.RequestParameters;

namespace PRAS.Contracts.Repositories
{
    public interface INewsRepository
    {
        Task<PagedList<News>> GetAllNewsAsync(RequestParameters.NewsRequestParameters requestParameters);
        Task<IEnumerable<News>> GetRecentNewsAsync();
        Task<News> GetNewsAsync(Guid newsId);
        Task<News> GetNewsAsTrackableAsync(Guid newsId);
        Task CreateNewsAsync(News news);
        void DeleteNews(News news);
    }
}
