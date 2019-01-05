using System;
using System.ComponentModel;
using System.Linq;

namespace AzureDevOpsCustomObjects
{
    public static class EnumExtension
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
    }
}