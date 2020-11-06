using System;

namespace Michaelsoft.BodyGuard.Common.BaseClasses
{
    /// <summary>
    /// This class provides a static Services properties that you "inject" in configure method with:
    ///    InjectableServicesBaseStaticClass.Services = app.ApplicationServices;
    /// </summary>
    public class InjectableServicesBaseStaticClass
    {

        private static IServiceProvider _services;

        /// <summary>
        /// Provides static access to the framework's services provider
        /// </summary>
        public static IServiceProvider Services
        {
            get => _services;
            set { _services ??= value; }
        }

    }
}