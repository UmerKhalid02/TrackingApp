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
using TrackingApp.Data.Entities.UserEntity;
using TrackingApp.Data.IRepositories.IAuthenticationRepository.IAuthenticationRepository;
using BC = BCrypt.Net.BCrypt;

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
            //await SaveRefreshToken("", refreshToken, user.UserId, "", model);

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
    }
}
