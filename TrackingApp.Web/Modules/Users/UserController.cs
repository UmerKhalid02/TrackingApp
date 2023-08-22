using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TrackingApp.Application.DataTransferObjects.UserDTO;
using TrackingApp.Application.Parameters;
using TrackingApp.Web.Modules.Common;

namespace TrackingApp.Web.Modules.Users
{
    [ApiController]
    [Route("api/v1/users")]
    public class UserController : BaseController
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [Authorize(Roles = "AD")]
        [HttpGet]
        public async Task<IActionResult> GetAllUsers([FromQuery] UserPageParamter request)
        {
            return Ok(await _userService.GetAllUsers(request));
        }

        [Authorize(Roles = "AD, US")]
        [HttpGet("{userId}")]
        public async Task<IActionResult> GetUserById(Guid userId)
        {
            return Ok(await _userService.GetUserById(userId));
        }

        [Authorize(Roles = "AD")]
        [HttpPost]
        public async Task<IActionResult> AddUser([FromBody] AddUserRequestDTO request)
        {
            return Ok(await _userService.AddUser(request));
        }

        [Authorize(Roles = "AD, US")]
        [HttpPut("{userId}")]
        public async Task<IActionResult> UpdateUser([FromBody] UpdateUserRequestDTO request, Guid userId)
        {
            return Ok(await _userService.UpdateUser(request, userId));
        }

        [Authorize(Roles = "AD, US")]
        [HttpDelete("{userId}")]
        public async Task<IActionResult> DeleteUser(Guid userId)
        {
            return Ok(await _userService.DeleteUser(userId));
        }
    }
}
