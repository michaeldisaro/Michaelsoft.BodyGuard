using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Michaelsoft.BodyGuard.Common.Enums;
using Michaelsoft.BodyGuard.Server.Settings;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Primitives;
using Microsoft.IdentityModel.Tokens;

namespace Michaelsoft.BodyGuard.Server.Services
{
    public class JwtMiddleware
    {

        private readonly RequestDelegate _next;

        private readonly JwtSettings _jwtSettings;

        public JwtMiddleware(RequestDelegate next,
                                  IOptions<JwtSettings> jwtSettings)
        {
            _next = next;
            _jwtSettings = jwtSettings.Value;
        }

        public async Task Invoke(HttpContext context)
        {
            context.Response.OnStarting(() =>
            {
                if (!(context.User.Identity is ClaimsIdentity identity) || !identity.IsAuthenticated)
                    return Task.CompletedTask;
                var token = CreateTokenForIdentity(identity);
                context.Response.Headers.Add(JwtBearerDefaults.AuthenticationScheme, token);
                return Task.CompletedTask;
            });
            await _next.Invoke(context);
        }

        private StringValues CreateTokenForIdentity(ClaimsIdentity identity)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Secret));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var payload = new JwtPayload
            {
                {"iss", _jwtSettings.Issuer},
                {"aud", _jwtSettings.Audience},
                {"exp", DateTimeOffset.UtcNow.AddMinutes(_jwtSettings.AccessExpiration).ToUnixTimeSeconds()}
            };

            var roles = new List<string>();
            foreach (var claim in identity.Claims)
            {
                if (claim.Type.Equals(ClaimTypes.NameIdentifier))
                    payload.TryAdd("sub", claim.Value);

                if (new[] {"sub"}.Contains(claim.Type))
                    payload.TryAdd(claim.Type, claim.Value);
                
                if (Claims.ClaimToUserProperty.Keys.Contains(claim.Type))
                    payload.TryAdd(claim.Type, claim.Value);

                if (new[] {"roles", ClaimTypes.Role}.Contains(claim.Type))
                    roles.Add(claim.Value);
            }

            payload.Add("roles", roles);

            var jwtToken = new JwtSecurityToken(new JwtHeader(credentials), payload);

            return new JwtSecurityTokenHandler().WriteToken(jwtToken);
        }

    }
}