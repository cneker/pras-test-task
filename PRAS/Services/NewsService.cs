using AutoMapper;
using PRAS.Contracts;
using PRAS.Contracts.Repositories;
using PRAS.Contracts.Services;
using PRAS.DataTransferObjects;
using PRAS.Exceptions;
using PRAS.Models;

namespace PRAS.Services
{
    public class NewsService : INewsService
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly IMapper _mapper;
        private readonly IFileManipulationService _fileService;

        public NewsService(IRepositoryManager repositoryManager, IMapper mapper, IFileManipulationService fileService)
        {
            _repositoryManager = repositoryManager;
            _mapper = mapper;
            _fileService = fileService;
        }

        public async Task<NewsDto> CreateNewsAsync(Guid userId, NewsForCreationDto newsDto)
        {
            var newsId = Guid.NewGuid();
            var imagePath = _fileService.SaveImageOnDisk(newsId.ToString(), newsDto.Base64String, newsDto.FileName);
            var news = _mapper.Map<News>(newsDto);
            news.ImagePath = imagePath;
            news.AuthorId = userId;

            await _repositoryManager.NewsRepository.CreateNewsAsync(news);
            await _repositoryManager.SaveAsync();

            var newsForReturn = _mapper.Map<NewsDto>(news);

            return newsForReturn;
        }

        public async Task<NewsDetailDto> GetNewsAsync(Guid id)
        {
            var news = await _repositoryManager.NewsRepository.GetNewsAsync(id);

            if (!IsNewsFound(news))
                throw new NotFoundException("News not found");

            return _mapper.Map<NewsDetailDto>(news);
        }

        public async Task<NewsPagingDto> GetAllNews(RequestParameters.NewsRequestParameters requestParameters)
        {
            var allNews = await _repositoryManager.NewsRepository.GetAllNewsAsync(requestParameters);

            var newsDto = new NewsPagingDto
            {
                MetaData = allNews.MetaData,
                AllNews = _mapper.Map<IEnumerable<NewsDto>>(allNews)
            };

            return newsDto;
        }

        public async Task<IEnumerable<NewsDto>> GetRecentNews()
        {
            var allNews = await _repositoryManager.NewsRepository.GetRecentNewsAsync();

            return _mapper.Map<IEnumerable<NewsDto>>(allNews);
        }

        public async Task DeleteNewsAsync(Guid id)
        {
            var news = await _repositoryManager.NewsRepository.GetNewsAsync(id);

            if (!IsNewsFound(news))
                throw new NotFoundException("News not found");

            _repositoryManager.NewsRepository.DeleteNews(news);
            await _repositoryManager.SaveAsync();
            _fileService.RemoveOldImage(news.ImagePath);
        }

        public async Task<NewsDetailDto> UpdateNewsAsync(Guid newsId, NewsForManipulationDto newsDto)
        {
            var news = await _repositoryManager.NewsRepository.GetNewsAsTrackableAsync(newsId);

            if (!IsNewsFound(news))
                throw new NotFoundException("News not found");

            if (news.ImagePath != newsDto.FileName)
            {
                _fileService.RemoveOldImage(news.ImagePath);
                var imagePath = _fileService.SaveImageOnDisk(newsId.ToString(), newsDto.Base64String, newsDto.FileName);
                news.ImagePath = imagePath;
            }

            news.Title = newsDto.Title;
            news.SubTitle = newsDto.SubTitle;
            news.Description = newsDto.Description;

            await _repositoryManager.SaveAsync();

            return _mapper.Map<NewsDetailDto>(news);
        }

        private bool IsNewsFound(News news) =>
            news != null;
    }
}
