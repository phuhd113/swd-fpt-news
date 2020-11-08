using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLL.IService;
using BLL.ViewModel.NewsModels;
using DAL.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace NewsFPT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NewsController : ControllerBase
    {
        private readonly INewsService _newsService;

        public NewsController(INewsService newsService)
        {
            _newsService = newsService;
        }
        [HttpGet("{id}")]
        public IActionResult GetNews(int id)
        {
            News news = _newsService.GetNewsById(id);
            if (news == null)
            {
                return NotFound("News is not found");
            }
            return Ok(news);
        }

        //   [Authorize(Roles = ClaimTypes.Role)]
        [HttpGet]
        public IActionResult GetAllNews()
        {
            //Console.WriteLine(ClaimTypes.Role);
            List<NewsViewModel> news = _newsService.GetAllNews().ToList();
            if (news == null)
            {
                return BadRequest("Error");
            }
            if (news.Count == 0)
            {
                return NotFound();
            }
            return Ok(news);
        }

        //[Authorize]
        [HttpPost]
        public IActionResult CreateNews(NewsViewModel newsCreate)
        {
            if (newsCreate == null)
            {
                return BadRequest("null");
            }
            bool check = _newsService.CreateNews(newsCreate);
            if (!check)
            {
                return BadRequest("Cannot create a new news");
            }
            return Ok("Success");
        }

       // [Authorize]
        [HttpPut]
        public IActionResult UpdateNews([FromBody] News news)
        {
            News news1 = _newsService.GetNewsById(news.NewsId);
            if (news1 != null)
            {
                return NoContent();

            }
            news1.NewsTitle = news.NewsTitle;
            news1.NewsContent = news.NewsContent;
            news1.LinkImage = news.LinkImage;
            news1.DayOfPost = news.DayOfPost;
            news1.ChannelId = news.ChannelId;
            return Ok("Update Successfully");
        }

      //  [Authorize]
        [HttpDelete("{id}")]
        public IActionResult DeleteNews(int id)
        {
            var check = _newsService.DeleteNews(id);
            if (!check)
            {
                return BadRequest("Error: Remove fail");
            }
            return Ok("Delete Successfully");
        }
    }
}
