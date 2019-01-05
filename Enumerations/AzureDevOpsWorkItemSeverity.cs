using System.ComponentModel;

namespace AzureDevOpsCustomObjects.Enumerations
{
    public enum AzureDevOpsWorkItemSeverity
    {
        [Description("1 - Critical")] Critical,
        [Description("2 - High")] High,
        [Description("3 - Medium")] Medium,
        [Description("4 - Low")] Low
    }
}