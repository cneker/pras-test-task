
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PRAS.Contracts.Services;
using PRAS.DataTransferObjects;
using PRAS.Services;
using PRAS.ViewModel;
using System.Security.Claims;
using ValidationException = PRAS.Exceptions.ValidationException;

namespace PRAS.Controllers
{
    [Authorize(Roles = "Admin")]
    [Route("/admin")]
    public class AdminController : Controller
    {
        private readonly INewsService _newsService;
        private readonly IValidator<NewsForManipulationDto> _validatorForUpdating;
        private readonly IValidator<NewsForCreationDto> _validatorForCreating;

        public AdminController(INewsService newsService, IValidator<NewsForManipulationDto> validatorForUpdating,
            IValidator<NewsForCreationDto> validatorForCreating)
        {
            _newsService = newsService;
            _validatorForUpdating = validatorForUpdating;
            _validatorForCreating = validatorForCreating;
        }


        [HttpGet]
        public async Task<IActionResult> Index([FromQuery] RequestParameters.NewsRequestParameters requestParameters)
        {
            var allNews = await _newsService.GetAllNews(requestParameters);
            Response.Headers.Add("X-Pagination",
                JsonConvert.SerializeObject(allNews.MetaData));

            if (requestParameters.PageNumber > 1)
            {
                return PartialView("_NewsPartial", allNews.AllNews);
            }

            var adminVM = new AdminViewModel
            {
                AllNews = allNews.AllNews,
                HasNext = allNews.MetaData.HasNext
            };

            return View(adminVM);
        }

        [HttpGet("news")]
        public async Task<IActionResult> News(Guid newsId)
        {
            var news = await _newsService.GetNewsAsync(newsId);

            return View(news);
        }

        [HttpPost]
        public async Task<IActionResult> CreateNews([FromBody] NewsForCreationDto newsDto)
        {
            var adminId = User.Claims.Single(c => c.Type == ClaimTypes.NameIdentifier).Value;

            var result = await _validatorForCreating.ValidateAsync(newsDto);
            if (!result.IsValid)
            {
                throw new ValidationException(ConvertHelper.ConvertErrorsToMessageString(result));
            }

            var news = await _newsService.CreateNewsAsync(Guid.Parse(adminId), newsDto);

            return Ok();
        }

        [HttpDelete("news")]
        public async Task<IActionResult> DeleteNews(Guid newsId)
        {
            await _newsService.DeleteNewsAsync(newsId);

            return NoContent();
        }

        [HttpPut("news")]
        public async Task<IActionResult> UpdateNews(Guid newsId, [FromBody] NewsForManipulationDto newsDto)
        {
            var result = await _validatorForUpdating.ValidateAsync(newsDto);
            if (!result.IsValid)
            {
                throw new ValidationException(ConvertHelper.ConvertErrorsToMessageString(result));
            }

            var news = await _newsService.UpdateNewsAsync(newsId, newsDto);

            return Ok(news);
        }
    }
}
