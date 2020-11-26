using System;
using System.Collections.Generic;
using System.Linq;
using Michaelsoft.BodyGuard.Client.Interfaces;
using Michaelsoft.BodyGuard.Common.BaseClasses;
using Microsoft.Extensions.DependencyInjection;

namespace Michaelsoft.BodyGuard.Client.Utilities
{
    public class BodyGuardAuthorizationUtility : InjectableServicesBaseStaticClass
    {

        public static bool AllowRole(string role)
        {
            return Allow(new List<string> {role}, null, false);
        }

        public static bool AllowRoles(string roles)
        {
            return Allow(roles.Split(",").ToList(), null, false);
        }

        public static bool AllowAny(List<string> roles = null,
                                    Dictionary<string, string> claims = null)
        {
            return Allow(roles, claims, false);
        }

        public static bool AllowAll(List<string> roles = null,
                                    Dictionary<string, string> claims = null)
        {
            return Allow(roles, claims, true);
        }

        private static bool Allow(List<string> roles = null,
                                  Dictionary<string, string> claims = null,
                                  bool checkAll = false)
        {
            try
            {
                using (var scope = Services.CreateScope())
                {
                    roles ??= new List<string>();
                    claims ??= new Dictionary<string, string>();
                    var loggedUserId = JwtUtility.GetUserId();
                    var authorizationApi = scope.ServiceProvider.GetService<IBodyGuardAuthorizationApiService>();
                    var response = authorizationApi.Can(loggedUserId, roles, claims, false).Result;
                    return response.Success;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

    }

}