using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLL.IService;
using BLL.ViewModel.NewsTagModel;
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
            bool check = _service.CreateNewsTag(newsTagModel);
            if (!check)
            {
                return BadRequest("Cannot create a new tag of news");
            }
            return Ok("Success");
        }          
    }
}
