using AzureDevOpsCustomObjects.Attributes;
using AzureDevOpsCustomObjects.Enumerations;

namespace AzureDevOpsCustomObjects.WorkItems
{
    public class AzureDevOpsProductBacklogItem : AzureDevOpsWorkItem
    {
        public AzureDevOpsProductBacklogItem()
        {
            DevOpsWorkItemType = AzureDevOpsWorkItemType.ProductBacklogItem;
        }

        [AzureDevOpsPath("/fields/Microsoft.VSTS.Common.ValueArea")]
        public string ValueArea { get; set; }

        [AzureDevOpsPath("/fields/Microsoft.VSTS.Common.BusinessValue")]
        public long BusinessValue { get; set; }

        [AzureDevOpsPath("/fields/System.Description")]
        public string Description { get; set; }

        [AzureDevOpsPath("/fields/Microsoft.VSTS.Scheduling.Effort")]
        public double? Effort { get; set; }
    }
}