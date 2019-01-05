using System.ComponentModel;

namespace AzureDevOpsCustomObjects.Enumerations
{
    public enum AzureDevOpsWorkItemPriority
    {
        [Description("1")] Top,
        [Description("2")] Medium,
        [Description("3")] Low
    }
}