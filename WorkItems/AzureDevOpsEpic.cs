using AzureDevOpsCustomObjects.Enumerations;

namespace AzureDevOpsCustomObjects.WorkItems
{
    public class AzureDevOpsEpic : AzureDevOpsWorkItem
    {
        public AzureDevOpsEpic()
        {
            DevOpsWorkItemType = AzureDevOpsWorkItemType.Epic;
        }
    }
}