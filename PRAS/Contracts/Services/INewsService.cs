using PRAS.DataTransferObjects;

namespace PRAS.Contracts.Services
{
    public interface INewsService
    {
        Task<NewsDto> CreateNewsAsync(Guid userId, NewsForCreationDto newsDto);
        Task DeleteNewsAsync(Guid id);
        Task<NewsDetailDto> UpdateNewsAsync(Guid newsId, NewsForManipulationDto newsDto);
        Task<NewsDetailDto> GetNewsAsync(Guid id);
        Task<NewsPagingDto> GetAllNews(RequestParameters.NewsRequestParameters requestParameters);
        Task<IEnumerable<NewsDto>> GetRecentNews();
    }
}
