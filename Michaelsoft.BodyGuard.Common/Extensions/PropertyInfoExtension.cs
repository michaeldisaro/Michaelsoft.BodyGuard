using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;

namespace Michaelsoft.BodyGuard.Common.Extensions
{
    public static class PropertyInfoExtension
    {

        public static string GetDisplayName(this PropertyInfo propertyInfo)
        {
            var displayAttribute =
                propertyInfo.CustomAttributes
                            .FirstOrDefault(cad => cad.AttributeType == typeof(DisplayAttribute));
            if (displayAttribute == null) return propertyInfo.Name;
            var displayNameAttribute =
                displayAttribute.NamedArguments?
                    .FirstOrDefault(cana => cana.MemberName == "Name");
            return displayNameAttribute == null
                       ? propertyInfo.Name
                       : displayNameAttribute.Value.TypedValue.Value!.ToString();
        }

    }
}