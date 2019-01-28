using System;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

namespace AzureDevOpsCustomObjects.Attributes
{
    public static class ReflectionExtensions
    {
        public static string GetDescription(this Enum severity)
        {
            var type = severity.GetType();

            var memInfo = type.GetMember(type.GetEnumName(severity));
            var descriptionAttribute = memInfo[0]
                .GetCustomAttributes(typeof(DescriptionAttribute), false)
                .Single() as DescriptionAttribute;

            return descriptionAttribute?.Description;
        }

        public static string GetFieldPath(this PropertyInfo propertyInfo)
        {
            var attribute = propertyInfo.GetCustomAttribute<AzureDevOpsPathAttribute>();

            return attribute?.Path;
        }

        public static object GetPropertyValue(this PropertyInfo propertyInfo, object parentObject)
        {
            if (!propertyInfo.PropertyType.IsEnum)
                return propertyInfo.GetValue(parentObject);

            var enumValue = propertyInfo.GetValue(parentObject) as Enum;

            return enumValue.GetDescription();
        }
    }
}