using System.ComponentModel;

namespace AzureDevOpsCustomObjects.Enumerations
{
    public enum AzureDevOpsWorkItemType
    {
        [Description("Bug")] Bug,
        [Description("Product Backlog Item")] ProductBacklogItem
    }
}