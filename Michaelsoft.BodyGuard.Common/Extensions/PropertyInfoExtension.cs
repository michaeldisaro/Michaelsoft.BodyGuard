using System.Linq;
using System.Reflection;
using Michaelsoft.BodyGuard.Common.Attributes;

namespace Michaelsoft.BodyGuard.Common.Extensions
{
    public static class PropertyInfoExtension
    {

        public static bool ShouldBeExcluded(this PropertyInfo propertyInfo)
        {
            return propertyInfo.CustomAttributes.Any(a => a.AttributeType == typeof(ExcludeFromForm));
        }

    }
}