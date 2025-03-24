using Laborotorna7.Models;
using Laborotorna7.Service;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

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

        [HttpGet("GetAllUsers", Name = "GetAllUsers")]
        [SwaggerOperation(
            Summary = "Отримати всіх користувачів",
            Description = "Повертає список усіх наявних користувачів"
        )]
        [SwaggerResponse(200, "Список користувачів успішно повернено", typeof(List<User>))]
        [SwaggerResponse(500, "Внутрішня помилка сервера")]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _userService.GetUsers();
            return Ok(users);
        }

        [HttpPost("CreateUser", Name = "CreateUser")]
        [SwaggerOperation(
            Summary = "Створити нового користувача",
            Description = "Створює нового користувача на основі переданих даних"
        )]
        [SwaggerResponse(200, "Користувач успішно створений", typeof(User))]
        [SwaggerResponse(400, "Передано порожній параметр")]
        [SwaggerResponse(404, "Користувача не знайдений")]
        [SwaggerResponse(500, "Внутрішня помилка сервера")]
        public async Task<IActionResult> CreateUser([FromBody] User user)
        {
            if (user == null)
            return BadRequest("Param null");

            var createdUser = await _userService.CreateUser(user);
            return Ok(createdUser);
        }

        [HttpPut("UpdateUser", Name = "UpdateUser")]
        [SwaggerOperation(
            Summary = "Оновити користувача",
            Description = "Оновлює дані існуючого користувача за його нікнеймом"
        )]
        [SwaggerResponse(200, "Користувач успішно оновлений", typeof(User))]
        [SwaggerResponse(400, "Передано порожній параметр")]
        [SwaggerResponse(404, "Користувача не знайдено")]
        [SwaggerResponse(500, "Внутрішня помилка сервера")]
        public async Task<IActionResult> UpdateUser(string nickName, [FromBody] User user)
        {
            var updatedUser = await _userService.UpdateUser(nickName, user);
            if (updatedUser == null)
                return NotFound("User not found");

            return Ok(updatedUser);
        }

        [HttpDelete("DeleteUser", Name = "DeleteUser")]
        [SwaggerOperation(
            Summary = "Видалити користувача",
            Description = "Видаляє користувача за його нікнеймом"
        )]
        [SwaggerResponse(200, "Користувач успішно видалений", typeof(string))]
        [SwaggerResponse(400, "Передано порожній параметр")]
        [SwaggerResponse(404, "Користувача не знайдено")]
        [SwaggerResponse(500, "Внутрішня помилка сервера")]
        public async Task<IActionResult> DeleteUser(string nickName)
        {
            var result = await _userService.DeleteUser(nickName);
            if (!result)
                return NotFound("User not found");

            return Ok("User deleted");
        }
    }
}
