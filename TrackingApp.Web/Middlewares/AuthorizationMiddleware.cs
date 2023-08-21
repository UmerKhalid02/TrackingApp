using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using NLog;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;
using TrackingApp.Application.DataTransferObjects.AuthenticationDTO.Authentication;
using TrackingApp.Application.DataTransferObjects.Shared;
using TrackingApp.Application.Enums;
using TrackingApp.Application.Wrappers;

namespace TrackingApp.Web.Middlewares
{
    public class AuthorizationMiddleware
    {
        private readonly RequestDelegate next;
        private readonly Logger logger;
        public AuthorizationMiddleware(RequestDelegate _next)
        {
            next = _next;
            logger = LogManager.GetCurrentClassLogger();
        }

        public async Task Invoke(HttpContext context)
        {
            NLogTrack logTrack = context.RequestServices.GetService(typeof(NLogTrack)) as NLogTrack;
            var endPoint = context.GetEndpoint();

            if (endPoint?.Metadata?.GetMetadata<IAllowAnonymous>() is object)
            {
                await next(context);
                return;
            }

            string token = string.Empty;
            token = context.Request.HttpContext.Request.Cookies[AuthCookiesValue.AuthKey];

            if (token == null)
            {
                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                await context.Response.WriteAsJsonAsync(new Response<dynamic>(false, "token is null"));

                logTrack.LogAccess("token is null");
                return;
            }

            if (token == string.Empty)
            {
                context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                await context.Response.WriteAsJsonAsync(new Response<dynamic>(false, "Unauthorized"));
                logTrack.LogAccess("Unauthorized");
                return;
            }

            var authToken = Newtonsoft.Json.JsonConvert.DeserializeObject<JwtTokenRequestDTO>(token);
            if (authToken == null || authToken.JwtToken == null)
            {
                context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                await context.Response.WriteAsJsonAsync(new Response<dynamic>(false, "Unauthorized"));
                logTrack.LogAccess("Unauthorized");
                return;
            }

            var key = Encoding.ASCII.GetBytes(JwtConfig.Secret);

            try
            {
                JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();

                TokenValidationParameters parameters = new TokenValidationParameters()
                {
                    RequireExpirationTime = true,
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = JwtConfig.ValidIssuer,
                    ValidAudience = JwtConfig.ValidAudience,
                    ValidateLifetime = true, //validate the expiration and not before values in the token
                    ClockSkew = TimeSpan.Zero //5 minute tolerance for the expiration date
                };
                ClaimsPrincipal principal = tokenHandler.ValidateToken(authToken.JwtToken, parameters, out SecurityToken securityToken);

                var claims = context.User.Identity as ClaimsIdentity;
                claims.AddClaims(principal.Claims);
                await next(context);
            }
            catch (Exception)
            {
                context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                await context.Response.WriteAsJsonAsync(new Response<dynamic>(false, "Unauthorized"));
                return;
            }
        }
    }
}
