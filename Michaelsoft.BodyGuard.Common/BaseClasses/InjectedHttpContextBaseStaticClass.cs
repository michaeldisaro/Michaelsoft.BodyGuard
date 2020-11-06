using Microsoft.AspNetCore.Http;

namespace Michaelsoft.BodyGuard.Common.BaseClasses
{
    /// <summary>
    /// This class provides a static HttpContext to derived classes if you add an IHttpContextAccessor singleton
    /// </summary>
    public class InjectedHttpContextBaseStaticClass : InjectableServicesBaseStaticClass
    {

        /// <summary>
        /// Provides static access to the current HttpContext
        /// </summary>
        public static HttpContext HttpContext
        {
            get
            {
                var httpContextAccessor =
                    Services.GetService(typeof(IHttpContextAccessor)) as IHttpContextAccessor;
                return httpContextAccessor?.HttpContext;
            }
        }

    }
}