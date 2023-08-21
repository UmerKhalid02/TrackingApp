using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using TrackingApp.Application.Constants;
using TrackingApp.Application.DataTransferObjects.AuthenticationDTO.Authentication;
using TrackingApp.Application.DataTransferObjects.Shared;
using TrackingApp.Application.Enums;
using TrackingApp.Application.Exceptions;
using TrackingApp.Data.Entities.UserEntity;
using TrackingApp.Data.IRepositories.IAuthenticationRepository.IAuthenticationRepository;
using BC = BCrypt.Net.BCrypt;
using FluentValidation.Results;

namespace TrackingApp.Data.Repositories.AuthenticationRepository.AuthenticationRepository
{
    public class AuthenticationRepository : IAuthenticationRepository
    {
        private readonly EFDataContext context;
        public AuthenticationRepository(EFDataContext context)
        {
            this.context = context;
        }
        public async Task<LoginResponseDTO> Authenticate(LoginModel model)
        {
            var user = await context.User.FirstOrDefaultAsync(x => (x.ContactNo == model.ContactNo || x.UserName == model.ContactNo) && x.IsActive == true && x.DeletedAt == null);

            if (user == null) {
                return null;
            }

            if (!BC.Verify(model.Password, user.Password)) {
                throw new UnauthorizedAccessException(GeneralMessages.UserLoggedInFailPassword);
            }

            var userRole = context.UserRole.Include(x => x.Role).FirstOrDefault(x => x.UserId == user.UserId && x.IsActive == true && x.DeletedAt == null);
            var newRefreshToken = GenerateRefreshToken();

            var tokenDescriptor = GetTokenDescriptor(user, userRole.Role.RoleName, DateTime.UtcNow.AddDays(20));

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);
            string refreshToken = await newRefreshToken;
            await SaveRefreshToken("", refreshToken, user.UserId, "", model);

            return new LoginResponseDTO()
            {
                UserId = user.UserId,
                Token = tokenString,
                RefreshToken = refreshToken,
            };
        }

        public async Task<string> GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            await Task.Run(() =>
            {
                using var rng = RandomNumberGenerator.Create();
                rng.GetBytes(randomNumber);
            });
            return Convert.ToBase64String(randomNumber);
        }

        private static SecurityTokenDescriptor GetTokenDescriptor(User user, string roleName, DateTime tokenTime)
        {
            var key = Encoding.ASCII.GetBytes(JwtConfig.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                 {
                    new Claim(ClaimsConstants.UserId, user.UserId.ToString()),
                    new Claim(ClaimsConstants.Email,user.Email),
                    new Claim(ClaimsConstants.ContactNo,user.ContactNo),
                    new Claim(ClaimTypes.Role,roleName),
                    new Claim(ClaimsConstants.ProjectScope,ClaimsConstants.ProjectScopeValue),
                 }),
                Expires = tokenTime, // expiry time
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            return tokenDescriptor;
        }

        public async Task<bool> SaveRefreshToken(string OldRefreshToken, string RefreshToken, Guid userId, string macAddress, LoginModel loginObj)
        {
            UserLogin? userLogin = await context.UserLogin.Where(m => m.RefreshToken == OldRefreshToken && m.UserId == userId).FirstOrDefaultAsync();
            if (userLogin != null)
            {
                userLogin.UserId = userId;
                userLogin.RefreshToken = RefreshToken;
                userLogin.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(25);
                userLogin.Status = "active";
                userLogin.RefreshTokenCreatedAt = DateTime.Now;
                userLogin.RefreshTokenUpdatedAt = DateTime.Now;
            }
            else
            {
                UserLogin obj = new()
                {
                    LogOutAt = DateTime.Now,
                    UserId = userId,
                    RefreshToken = RefreshToken,
                    RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(25),
                    RefreshTokenUpdatedAt = DateTime.Now,
                    RefreshTokenCreatedAt = DateTime.Now,
                    Status = "active"
                };
                await context.UserLogin.AddAsync(obj);
            }
            await context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Logout(LogoutRequestModel model)
        {
            try
            {
                IList<FluentValidation.Results.ValidationFailure> errorMessages = new List<FluentValidation.Results.ValidationFailure>();
                UserLogin userSession = await context.UserLogin.Where(m => m.UserId == model.UserId).FirstOrDefaultAsync();
                if (userSession != null)
                {
                    userSession.LogOutAt = DateTime.UtcNow;
                    userSession.Status = "inactive";
                    userSession.IsActive = false;
                    userSession.RefreshTokenExpiryTime = DateTime.UtcNow;
                    await context.SaveChangesAsync();
                    return true;
                }

                errorMessages.Add(new FluentValidation.Results.ValidationFailure("EmployeeId", "Incorrect EmployeeId please check!"));
                errorMessages.Add(new FluentValidation.Results.ValidationFailure("Token", "Incorrect token please check!"));
                ErrorModel error = new()
                {
                    latestError = "Incorrect token please check!"
                };

                if (errorMessages.Count > 0) throw new ValidationException(errorMessages);
                return false;
            }
            catch (Exception ex)
            {
                throw new ApiException(ex.Message);
            }
        }
    }
}
