using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using Michaelsoft.BodyGuard.Common.BaseClasses;
using Michaelsoft.BodyGuard.Common.Extensions;

namespace Michaelsoft.BodyGuard.Common.Utilities
{
    public class JwtUtility : InjectedHttpContextBaseStaticClass
    {

        public static string GetUserClaim(string userClaim)
        {
            HttpContext.Request.Cookies.TryGetValue("bearer", out var bearer);
            if (bearer == null) return null;
            var jwt = new JwtSecurityTokenHandler().ReadToken(bearer) as JwtSecurityToken;
            if (jwt!.ValidTo.CompareTo(DateTime.UtcNow) < 0) return null;
            if (userClaim.IsNullOrEmpty())
                return jwt!.Subject.Substring(jwt.Subject.Length - 6);
            return jwt!.Claims.FirstOrDefault(c => c.Type == userClaim)?.Value ??
                   jwt!.Subject.Substring(jwt.Subject.Length - 6);
        }

    }
}