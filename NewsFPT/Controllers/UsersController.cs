using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLL.IService;
using DAL.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace NewsFPT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly NewsFPTContext _context;
        private readonly IUserService _userService;
        public UsersController(NewsFPTContext context, IUserService service)
        {
            _context = context;
            _userService = service;
        }
        [HttpGet]
        public IActionResult GetAllUsers()
        {
            //Console.WriteLine(ClaimTypes.Role);
            List<User> users = _userService.GetAllUsers().ToList();
            if (users == null)
            {
                return BadRequest("Error");
            }
            if (users.Count == 0)
            {
                return NotFound();
            }
            return Ok(users);
        }

        [HttpPost]
        public IActionResult CreateUser(User user)
        {
            if (user == null)
            {
                return BadRequest("null");
            }
            bool check = _userService.CreateUser(user);
            if (!check)
            {
                return BadRequest("Cannot create a new user");
            }
            return Ok(user);
        }
    }
}
