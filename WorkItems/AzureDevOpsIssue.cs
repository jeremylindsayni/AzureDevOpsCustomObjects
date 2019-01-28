using AzureDevOpsCustomObjects.Enumerations;

namespace AzureDevOpsCustomObjects.WorkItems
{
    public class AzureDevOpsIssue : AzureDevOpsWorkItem
    {
        public AzureDevOpsIssue()
        {
            DevOpsWorkItemType = AzureDevOpsWorkItemType.Issue;
        }
    }
}