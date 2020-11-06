using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;

namespace Michaelsoft.BodyGuard.Common.BaseClasses
{
    /// <summary>
    /// This class provides a static Localizer to derived classes if you add an ILocalizer scoped
    /// </summary>
    public class InjectedLocalizerBaseStaticClass : InjectableServicesBaseStaticClass
    {

        /// <summary>
        /// Provides static access to the current Localizer
        /// </summary>
        public static IStringLocalizer Localizer
        {
            get
            {
                using (var scope = Services.CreateScope())
                {
                    var localizer =
                        scope.ServiceProvider.GetService(typeof(IStringLocalizer)) as IStringLocalizer;
                    return localizer;
                }
            }
        }

    }
}