using Microsoft.AspNetCore.Mvc;
using TrackingApp.Application.Enums;
using TrackingApp.Application.Wrappers;

namespace TrackingApp.Web.Modules.Common
{
    [ApiController]
    [Produces("application/json")]
    [ProducesResponseType(typeof(Response<>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(DefaultApiConventions), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(Response<>), StatusCodes.Status400BadRequest)]
    public class BaseController : ControllerBase
    {
        protected Guid GetUserId()
        {
            try
            {
                Guid userId = Guid.Parse(User.Claims.First(i => i.Type == UserKey.UserID).Value);
                return userId;
            }
            catch (Exception)
            {
                throw new Exception(GeneralMessages.InvalidToken);
            }
        }
    }
}
