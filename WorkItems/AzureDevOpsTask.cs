using AzureDevOpsCustomObjects.Enumerations;

namespace AzureDevOpsCustomObjects.WorkItems
{
    public class AzureDevOpsTask : AzureDevOpsWorkItem
    {
        public AzureDevOpsTask()
        {
            DevOpsWorkItemType = AzureDevOpsWorkItemType.Task;
        }
    }
}