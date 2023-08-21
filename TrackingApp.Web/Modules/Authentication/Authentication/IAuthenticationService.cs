using TrackingApp.Application.DataTransferObjects.AuthenticationDTO.Authentication;
using TrackingApp.Application.Wrappers;

namespace TrackingApp.Web.Modules.Authentication.Authentication
{
    public interface IAuthenticationService
    {
        Task<Response<LoginResponseDTO>> AuthenticateService(LoginModel model);
    }
}
