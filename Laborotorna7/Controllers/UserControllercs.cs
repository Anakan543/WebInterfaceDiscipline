using Laborotorna7.Models;
using Laborotorna7.Service;
using Microsoft.AspNetCore.Mvc;

namespace Laborotorna7.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserControllercs : ControllerBase
    {
        private readonly IUserService _userService;

        public UserControllercs(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet(Name = "GetAllUsers")]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _userService.GetUsers();
            return Ok(users);
        }

        [HttpPost(Name = "CreateUser")]
        public async Task<IActionResult> CreateUser([FromBody] User user)
        {
            if (user == null)
            return BadRequest("Param null");

            var createdUser = await _userService.CreateUser(user);
            return Ok(createdUser);
        }

        [HttpPut(Name = "UpdateUser")]
        public async Task<IActionResult> UpdateUser(string nickName, [FromBody] User user)
        {
            var updatedUser = await _userService.UpdateUser(nickName, user);
            if (updatedUser == null)
                return NotFound("User not found");

            return Ok(updatedUser);
        }

        [HttpDelete(Name = "DeleteUser")]
        public async Task<IActionResult> DeleteUser(string nickName)
        {
            var result = await _userService.DeleteUser(nickName);
            if (!result)
                return NotFound("User not found");

            return Ok("User deleted");
        }
    }
}
