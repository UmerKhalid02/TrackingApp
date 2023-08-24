using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TrackingApp.Application.DataTransferObjects.AuthenticationDTO.Authentication;
using TrackingApp.Web.Modules.Common;

namespace TrackingApp.Web.Modules.Authentication.Authentication
{
    [Route("api/v1/authentication")]
    public class AuthenticationController : BaseController
    {
        private readonly IAuthenticationService service;
        public AuthenticationController(IAuthenticationService service)
        {
            this.service = service;
        }


        [AllowAnonymous]
        [HttpPost("authenticate")]
        public async Task<IActionResult> Authenticate([FromBody] LoginRequestModel userDto)
        {
            LoginModel model = new()
            {
                UserName = userDto.UserName,
                Password = userDto.Password
            };
            return Ok(await service.AuthenticateService(model));
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequestModel request)
        {
            return Ok(await service.RegisterService(request));
        }

        [Authorize(Roles = "AD, US")]
        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            Guid userId = GetUserId();
            LogoutRequestModel model = new()
            {
                UserId = userId
            };
            return Ok(await service.LogoutService(model));
        }
    }
}
