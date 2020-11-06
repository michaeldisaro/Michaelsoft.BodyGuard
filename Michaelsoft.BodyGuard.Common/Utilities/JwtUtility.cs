using System.IdentityModel.Tokens.Jwt;
using Michaelsoft.BodyGuard.Common.BaseClasses;

namespace Michaelsoft.BodyGuard.Common.Utilities
{
    public class JwtUtility : InjectedHttpContextBaseStaticClass
    {

        public static string GetSubscriber()
        {
            HttpContext.Request.Cookies.TryGetValue("bearer", out var bearer);
            if (bearer == null) return null;
            var jwt = new JwtSecurityTokenHandler().ReadToken(bearer) as JwtSecurityToken;
            return jwt!.Subject.Substring(0, 6);
        }

    }
}