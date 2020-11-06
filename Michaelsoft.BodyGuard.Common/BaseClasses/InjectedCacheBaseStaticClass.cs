using Microsoft.Extensions.Caching.Distributed;

namespace Michaelsoft.BodyGuard.Common.BaseClasses
{
    /// <summary>
    /// This class provides a static HttpContext to derived classes if you add an IHttpContextAccessor singleton
    /// </summary>
    public class InjectedCacheBaseStaticClass : InjectableServicesBaseStaticClass
    {

        /// <summary>
        /// Provides static access to the current HttpContext
        /// </summary>
        public static IDistributedCache Cache
        {
            get
            {
                var cache =
                    Services.GetService(typeof(IDistributedCache)) as IDistributedCache;
                return cache;
            }
        }
        
        

    }
}