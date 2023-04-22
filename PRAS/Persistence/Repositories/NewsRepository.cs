using Microsoft.EntityFrameworkCore;
using PRAS.Contracts.Repositories;
using PRAS.Models;
using PRAS.RequestParameters;

namespace PRAS.Persistence.Repositories
{
    public class NewsRepository : RepositoryBase<News>, INewsRepository
    {
        public NewsRepository(AppDbContext context) : base(context)
        {
        }

        public void DeleteNews(News news) =>
            Delete(news);

        public async Task<PagedList<News>> GetAllNewsAsync(RequestParameters.NewsRequestParameters requestParameters)
        {
            var list = await GetAll(false).OrderByDescending(n => n.PublicationDate)
                .ToListAsync();
            return PagedList<News>.ToPagedList(list, requestParameters.PageSize, requestParameters.PageNumber);
        }


        public async Task<IEnumerable<News>> GetRecentNewsAsync() =>
            await GetAll(false).OrderByDescending(n => n.PublicationDate)
                .Take(6)
                .ToListAsync();

        public async Task<News> GetNewsAsync(Guid newsId) =>
            await GetByCondition(n => n.Id == newsId, false).Include(n => n.Author)
                .SingleOrDefaultAsync();

        public async Task<News> GetNewsAsTrackableAsync(Guid newsId) =>
            await GetByCondition(n => n.Id == newsId, true).Include(n => n.Author)
                .SingleOrDefaultAsync();

        public async Task CreateNewsAsync(News news) =>
            await CreateAsync(news);
    }
}
