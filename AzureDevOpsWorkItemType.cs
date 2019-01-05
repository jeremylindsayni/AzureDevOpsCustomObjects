using System.ComponentModel;

namespace AzureDevOpsCustomObjects
{
    public enum AzureDevOpsWorkItemType
    {
        [Description("Bug")] Bug,
        [Description("Task")] Task
    }
}