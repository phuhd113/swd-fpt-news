using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLL.IService;
using BLL.ViewModel.ChannelsModel;
using DAL.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace NewsFPT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChannelsController : ControllerBase
    {
        private readonly IChannelService _service;

        public ChannelsController(IChannelService service)
        {
            _service = service;
        }

        [HttpGet("{id}")]
        public IActionResult GetChannel(int id)
        {
            Channel channel = _service.GetChannelById(id);
            if (channel == null)
            {
                return NotFound("Channel is not found");
            }
            return Ok(channel);
        }
        [HttpGet]
        public IActionResult GetAllChannels()
        {
            List<Channel> channels = _service.GetAllChannels().ToList();
            if (channels == null)
            {
                return BadRequest("Error");
            }
            if (channels.Count == 0)
            {
                return NotFound();
            }
            return Ok(channels);
        }

        //[Authorize]
        [HttpPost]
        public IActionResult CreateChannel(ChannelCreateModel channelCreate)
        {
            if (channelCreate == null)
            {
                return BadRequest("null");
            }
            bool check = _service.CreateNewChannel(channelCreate);
            if (!check)
            {
                return BadRequest("Cannot create a new channel");
            }
            return Ok("Success");
        }


        //[Authorize(Policy = "RequireAdminRole")]
        [HttpPut]
        public IActionResult UpdateChannel([FromBody] Channel channel)
        {
            Channel channel1 = _service.GetChannelById(channel.ChannelId);
            if (channel1 != null)
            {
                return NoContent();

            }
            channel1.ChannelName = channel.ChannelName;
            channel1.IsActive = channel.IsActive;
            channel1.GroupId = channel.GroupId;
            _service.UpdateChannel(channel);
            return Ok("Update Successfully");
        }

        //[Authorize(Policy = "RequireAdminRole")]
        [HttpDelete("{id}")]
        public IActionResult DeleteChannel(int id)
        {
            var check = _service.DeleteChannel(id);
            if (!check)
            {
                return BadRequest("Error: Remove fail");
            }
            return Ok("Delete Successfully");
        }
    }
}
