using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using DMSapi.Models;
using DMSapi.Services;
using System.Net.Http.Headers;
using System.IO;
using System.Linq;

namespace DMSapi.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]

    public class UserController : ControllerBase
    {
        private readonly UserService _userService;
        public UserController(UserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public ActionResult<List<User>> GetAllUser() => _userService.GetUserAll();

        [HttpGet("{id}")]
        public ActionResult<User> GetUserById(string id)
        {
            var user = _userService.GetUserById(id);
            if(user == null){
                return NotFound();
            }
            return user;
        }

        [HttpPost]
        public User CreateUser([FromBody] User user)
        {
            var data = _userService.GetUserAlls();
            var count = data.Count();
            var id = "U00" + count.ToString();
            user.UserId = id;
            user.Status = "Open";
            _userService.CreateUser(user);
            return user;
        }

        [HttpPut("{id}")]
        public IActionResult EditUser([FromBody] User user, string id)
        {
            var users = _userService.GetUserById(id);
            if(users == null){
                return NotFound();
            }
            users.UserId = id;
            _userService.EditUser(id,user);
            return NoContent();
        }

        [HttpGet("{id}")]
        public IActionResult DeleteUser(string id)
        {
            var user = _userService.GetUserById(id);
            var statusChange = user.Status;
            if( user == null){
                return NotFound();
            }
            if(statusChange == "Open"){
                 statusChange = "Close";
            }
            user.Status = statusChange;
            _userService.DeleteUser(id, user);
            return NoContent();
        }
        
    }
}