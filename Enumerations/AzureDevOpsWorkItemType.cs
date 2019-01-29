using System.ComponentModel;

namespace AzureDevOpsCustomObjects.Enumerations
{
    public enum AzureDevOpsWorkItemType
    {
        [Description("Bug")] Bug,
        [Description("Epic")] Epic,
        [Description("Feature")] Feature,
        [Description("Impediment")] Impediment,
        [Description("Product Backlog Item")] ProductBacklogItem,
        [Description("Task")] Task,
        [Description("Test Case")] TestCase
    }
}