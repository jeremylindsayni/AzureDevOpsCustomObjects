using AzureDevOpsCustomObjects.Enumerations;

namespace AzureDevOpsCustomObjects.WorkItems
{
    public class AzureDevOpsFeature : AzureDevOpsWorkItem
    {
        public AzureDevOpsFeature()
        {
            DevOpsWorkItemType = AzureDevOpsWorkItemType.Feature;
        }
    }
}