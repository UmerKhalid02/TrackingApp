using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TrackingApp.Application.DataTransferObjects.AuthenticationDTO.Authentication;

namespace TrackingApp.Web.Modules.Authentication.Authentication
{
    [ApiController]
    [Route("api/v1/authentication")]
    public class AuthenticationController : ControllerBase
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
                ContactNo = userDto.ContactNo,
                Password = userDto.Password
            };
            return Ok(await service.AuthenticateService(model));
        }
    }
}
