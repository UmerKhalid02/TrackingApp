using Microsoft.AspNetCore.Mvc;
using TrackingApp.Application.DataTransferObjects.UserDTO;
using TrackingApp.Application.Parameters;

namespace TrackingApp.Web.Modules.Users
{
    [ApiController]
    [Route("api/v1/users")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUsers([FromQuery] UserPageParamter request)
        {
            return Ok(await _userService.GetAllUsers(request));
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetUserById(Guid userId)
        {
            return Ok(await _userService.GetUserById(userId));
        }

        [HttpPost]
        public async Task<IActionResult> AddUser([FromBody] AddUserRequestDTO request)
        {
            return Ok(await _userService.AddUser(request));
        }



    }
}
