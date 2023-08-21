using TrackingApp.Application.DataTransferObjects.AuthenticationDTO.Authentication;


namespace TrackingApp.Data.IRepositories.IAuthenticationRepository.IAuthenticationRepository
{
    public interface IAuthenticationRepository
    {
        Task<LoginResponseDTO> Authenticate(LoginModel model);
        Task<bool> Logout(LogoutRequestModel model);
    }
}
