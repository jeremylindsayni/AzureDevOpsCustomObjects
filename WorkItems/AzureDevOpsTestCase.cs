using AzureDevOpsCustomObjects.Enumerations;

namespace AzureDevOpsCustomObjects.WorkItems
{
    public class AzureDevOpsTestCase : AzureDevOpsWorkItem
    {
        public AzureDevOpsTestCase()
        {
            DevOpsWorkItemType = AzureDevOpsWorkItemType.TestCase;
        }
    }
}