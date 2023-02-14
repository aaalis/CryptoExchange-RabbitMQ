using Microsoft.AspNetCore.Mvc;
using Users.Model;
using Users.Services;
using Users.Model.Dto;
using System.Linq;

namespace Users.Controllers
{
    [ApiController]
    [Route("UsersAPI/v1/[action]")]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        private readonly IUserService _userService;

        public UserController(ILogger<UserController> logger, IUserService userService)
        {
            _logger = logger;
            _userService = userService;
        }

        [HttpPost]
        public async Task<ActionResult<UserDto>> CreateUser([FromBody] User user) 
        {
            UserDto userDto = await _userService.CreateUser(user);
            return Ok(userDto);
        }

        [HttpPost]
        public async Task CreateUsers([FromBody] IEnumerable<User> users)
        {
            await _userService.CreateUsers(users);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserDto>> GetUserById(int id)
        {
            UserDto user = await _userService.GetUserById(id);
            if (user == null)
            {
                return NotFound($"User with id:{id} not found");
            }
            return Ok(user);
        }

        [HttpGet("{login}")]
        public async Task<ActionResult<UserDto>> GetUserByLogin(string login)
        {
            UserDto userDto = await _userService.GetUserByLogin(login);
            if (userDto == null)
            {
                return NotFound($"User with login:{login} not found");
            }
            return Ok(userDto);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserDto>>> GetUsersById([FromQuery(Name = "usersId")] List<int> ids)
        {
            var users = await _userService.GetUsersById(ids);
            if (!users.Any())
            {
                return NotFound("Users not found");
            }
            return Ok(users);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetFullFieldUser(int id)
        {
            User user = await _userService.GetFullFieldsUser(id);
            if (user == null)
            {
                return NotFound($"User with id:{id} not found");
            }
            return Ok(user);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<UserDto>> DeleteUser(int id)
        {
            return Ok(await _userService.DeleteUser(id));
        }

        [HttpDelete]
        public async Task<ActionResult> DeleteUsers([FromBody] IEnumerable<int> ids)
        {
            await _userService.DeleteUsers(ids);
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<UserDto>> UpdateUser(int id, [FromBody] User user)
        {
            return Ok(await _userService.UpdateUser(id, user));
        }

        [HttpPut("{id}&{name}")]
        public async Task<ActionResult<UserDto>> UpdateUserName(int id, string name)
        {
            UserDto user = await _userService.UpdateUserName(id, name);
            if (user == null)
            {
                _logger.LogInformation($"User with id:{id} not found");
                return NotFound($"User with id:{id} not found");
            }
            return Ok(user);
        }
    }
}