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

        public TagsController(ITagService tagService)
        {
            _tagService = tagService;
        }
        [HttpPost]
        public IActionResult CreateTag(string tagName)
        {
            if(tagName != null)
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
            if( id != 0)
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

    }
}
