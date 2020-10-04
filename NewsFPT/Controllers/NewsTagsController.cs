using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLL.IService;
using BLL.Models.NewsTagModel;
using DAL.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace NewsFPT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NewsTagsController : ControllerBase
    {
        private readonly INewsTagService _service;

        public NewsTagsController(INewsTagService service)
        {
            _service = service;
        }
        [HttpPost]
        public IActionResult CreateNewsTag(NewsTagModel newsTagModel)
        {
            if (newsTagModel == null)
            {
                return BadRequest("null");
            }
            bool check = _service.CreateTagNews(newsTagModel);
            if (!check)
            {
                return BadRequest("Cannot create a new tag of news");
            }
            return Ok("Success");
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteNewsTag(int id)
        {
            var check = _service.DeleteTagNews(id);
            if (!check)
            {
                return BadRequest("Error: Remove fail");
            }
            return Ok("Delete Successfully");
        }

        [HttpGet]
        public IActionResult GetNewsTag()
        {
            List<NewsTag> newsTag = _service.GetNewsTag().ToList();
            if (newsTag == null)
            {
                return BadRequest("Error");
            }
            if (newsTag.Count == 0)
            {
                return NotFound();
            }
            return Ok(newsTag);
            //comment
        }

        [HttpGet("{id}")]
        public IActionResult GetNewsTagById(int id)
        {
            NewsTag newsTag = _service.GetModelsById(id);
            if (newsTag == null)
            {
                return NotFound("News tag is not found");
            }
            return Ok(newsTag);
        }


    }
}
