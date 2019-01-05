using AzureDevOpsCustomObjects.Enumerations;

namespace AzureDevOpsCustomObjects.WorkItems
{
    public class AzureDevOpsBug : AzureDevOpsWorkItem
    {
        public AzureDevOpsBug()
        {
            DevOpsWorkItemType = AzureDevOpsWorkItemType.Bug;
        }
    }
}