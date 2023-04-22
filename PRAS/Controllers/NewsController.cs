using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PRAS.Contracts.Services;
using PRAS.ViewModel;

namespace PRAS.Controllers
{
    public class NewsController : Controller
    {
        private readonly INewsService _newsService;

        public NewsController(INewsService newsService)
        {
            _newsService = newsService;
        }

        [HttpGet]
        public async Task<IActionResult> News(Guid newsId)
        {
            var news = await _newsService.GetNewsAsync(newsId);

            return View(news);
        }

        [HttpGet]
        public async Task<IActionResult> AllNews([FromQuery] RequestParameters.NewsRequestParameters requestParameters)
        {
            var allNews = await _newsService.GetAllNews(requestParameters);
            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(allNews.MetaData));

            if (requestParameters.PageNumber > 1)
            {
                return PartialView("_AllNewsPartial", allNews.AllNews);
            }

            var newsVM = new AllNewsViewModel
            {
                AllNews = allNews.AllNews,
                HasNext = allNews.MetaData.HasNext
            };

            return View(newsVM);
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var recentNews = await _newsService.GetRecentNews();

            return View(recentNews);
        }
    }
}
