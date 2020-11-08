using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLL.IService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace NewsFPT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TagsController : ControllerBase
    {
        private readonly ITagService _tagService;
        private readonly INewsTagService _newsTagService;


        public TagsController(ITagService tagService, INewsTagService newsTagService)
        {
            _tagService = tagService;
            _newsTagService = newsTagService;
        }
        [HttpPost]
        public IActionResult CreateTag(string tagName)
        {
            if (tagName != null)
            {
                if (_tagService.CreateTag(tagName))
                {
                    return Ok("Create Tag Success");
                }
                else
                {
                    return BadRequest("Cannot create tag");
                }
            }
            return BadRequest("null");
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteTag(int id)
        {
            if (id != 0)
            {
                if (_tagService.DeleteTagByID(id))
                {
                    return Ok("Delete Tag Success");
                }
                else
                {
                    return BadRequest("Cannot Delete tag");
                }

            }
            return BadRequest("null");
        }

        [HttpGet("{newsId}")]
        public IActionResult GetTagsByNewsId(int newsId)
        {
            if (newsId != 0)
            {
                var tags = _newsTagService.GetTagsByNewsId(newsId).ToList();
                if (tags.Any())
                {
                    return Ok(tags);
                }
                else
                {
                    return BadRequest(tags);
                }
            }
            return null;
        }
        [HttpGet]
        public IActionResult GetAllTag()
        {
            var tags = _tagService.GetAllTag();
            if (tags.Any())
            {
                return Ok(tags);
            }
            else
            {
                return BadRequest(tags);
            }
        }

    }
}
