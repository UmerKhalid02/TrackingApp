using NLog;
using TrackingApp.Application.DataTransferObjects.AuthenticationDTO.Authentication;
using TrackingApp.Application.DataTransferObjects.Shared;
using TrackingApp.Application.Enums;
using TrackingApp.Application.Exceptions;
using TrackingApp.Application.Wrappers;
using TrackingApp.Data.IRepositories.IAuthenticationRepository.IAuthenticationRepository;

namespace TrackingApp.Web.Modules.Authentication.Authentication
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IAuthenticationRepository _authenticationRepository;
        private readonly IHttpContextAccessor httpContext;
        private readonly NLogTrack nlogTrack;
        public AuthenticationService(IAuthenticationRepository authenticationRepository, NLogTrack nlogTrack, IHttpContextAccessor httpContext)
        {
            _authenticationRepository = authenticationRepository;
            this.nlogTrack = nlogTrack;
            this.nlogTrack.logger = LogManager.GetCurrentClassLogger();
            this.httpContext = httpContext;
        }
        public async Task<Response<LoginResponseDTO>> AuthenticateService(LoginModel model)
        {
            var result = await _authenticationRepository.Authenticate(model);
            if(result == null)
                throw new BadRequestException(GeneralMessages.InvalidCredentials);

            JwtTokenRequestDTO refreshTokenRequestModel = new()
            {
                JwtToken = result.Token,
                RefreshToken = result.RefreshToken
            };

            AddAuthenticationCookies(refreshTokenRequestModel, DateTime.UtcNow.AddDays(20));
            nlogTrack.LogAccess("Logged In Successfull");
            return new Response<LoginResponseDTO>(true, result, GeneralMessages.UserLoggedInSuccessMessage);

        }

        public bool AddAuthenticationCookies(JwtTokenRequestDTO token, DateTime expiryTime)
        {
            if (token == null)
                throw new Exception(GeneralMessages.TokenIssue);

            CookieOptions option = new()
            {
                Expires = expiryTime,
                Secure = true,
                SameSite = SameSiteMode.None,
                Path = "/",
                HttpOnly = true
            };

            var authToken = Newtonsoft.Json.JsonConvert.SerializeObject(token);

            httpContext.HttpContext.Response.Cookies.Append(AuthCookiesValue.AuthKey, authToken, option);
            return true;
        }

        public async Task<Response<bool>> LogoutService(LogoutRequestModel model)
        {
            dynamic result = await _authenticationRepository.Logout(model);
            if (result)
            {
                httpContext.HttpContext.Response.Cookies.Delete(AuthCookiesValue.AuthKey);
                nlogTrack.LogAccess("Logout Successfull");
                return new Response<bool>(true, true, GeneralMessages.UserLogoutSuccessMessage);
            }
            else
            {
                nlogTrack.LogAccess("Logout Failed");
                return new Response<bool>(false, false, GeneralMessages.UserLogoutFailMessage);
            }
        }
    }
}
