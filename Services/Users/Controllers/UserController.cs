using Microsoft.AspNetCore.Mvc;
using Users.Model;
using Users.Services;
using Users.Model.Dto;

namespace Users.Controllers
{
    [ApiController]
    [Route("api/v1/")]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        private readonly IUserService _userService;

        public UserController(ILogger<UserController> logger, IUserService userService)
        {
            _logger = logger;
            _userService = userService;
        }

        [HttpPost("[action]")]
        public async Task<ActionResult<UserDto>> CreateUser([FromBody] User user) 
        {
            UserDto userDto = await _userService.CreateUser(user);

            return Ok(userDto);
        }
    }
}