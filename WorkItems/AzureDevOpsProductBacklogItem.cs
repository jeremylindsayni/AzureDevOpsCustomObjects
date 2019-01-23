using AzureDevOpsCustomObjects.Enumerations;

namespace AzureDevOpsCustomObjects.WorkItems
{
    public class AzureDevOpsProductBacklogItem : AzureDevOpsWorkItem
    {
        public AzureDevOpsProductBacklogItem()
        {
            DevOpsWorkItemType = AzureDevOpsWorkItemType.ProductBacklogItem;
        }
    }
}