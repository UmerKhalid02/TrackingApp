using AutoMapper;
using NLog;
using TrackingApp.Application.DataTransferObjects.AuthenticationDTO.Authentication;
using TrackingApp.Application.DataTransferObjects.Shared;
using TrackingApp.Application.DataTransferObjects.UserDTO;
using TrackingApp.Application.Enums;
using TrackingApp.Application.Exceptions;
using TrackingApp.Application.Wrappers;
using TrackingApp.Data.IRepositories.IAuthenticationRepository.IAuthenticationRepository;
using TrackingApp.Web.Modules.Users;

namespace TrackingApp.Web.Modules.Authentication.Authentication
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IAuthenticationRepository _authenticationRepository;
        private readonly IHttpContextAccessor _httpContext;
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        private readonly NLogTrack _nlogTrack;
        public AuthenticationService(IAuthenticationRepository authenticationRepository, IUserService userService, IMapper mapper, NLogTrack nlogTrack, IHttpContextAccessor httpContext)
        {
            _authenticationRepository = authenticationRepository;
            this._userService = userService;
            this._nlogTrack = nlogTrack;
            this._mapper = mapper;
            this._nlogTrack.logger = LogManager.GetCurrentClassLogger();
            this._httpContext = httpContext;
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
            _nlogTrack.LogAccess("Logged In Successfull");
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

            _httpContext.HttpContext.Response.Cookies.Append(AuthCookiesValue.AuthKey, authToken, option);
            return true;
        }

        public async Task<Response<bool>> LogoutService(LogoutRequestModel model)
        {
            dynamic result = await _authenticationRepository.Logout(model);
            if (result)
            {
                _httpContext.HttpContext.Response.Cookies.Delete(AuthCookiesValue.AuthKey);
                _nlogTrack.LogAccess("Logout Successfull");
                return new Response<bool>(true, true, GeneralMessages.UserLogoutSuccessMessage);
            }
            else
            {
                _nlogTrack.LogAccess("Logout Failed");
                return new Response<bool>(false, false, GeneralMessages.UserLogoutFailMessage);
            }
        }

        public async Task<Response<bool>> RegisterService(RegisterRequestModel request)
        {
            var userRequest = _mapper.Map<AddUserRequestDTO>(request);

            await _userService.AddUser(userRequest);
            return new Response<bool>(true, true, GeneralMessages.RegisterUserSuccess);
        }
    }
}
