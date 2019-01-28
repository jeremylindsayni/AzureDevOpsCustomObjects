using AzureDevOpsCustomObjects.Enumerations;

namespace AzureDevOpsCustomObjects.WorkItems
{
    public class AzureDevOpsImpediment : AzureDevOpsWorkItem
    {
        public AzureDevOpsImpediment()
        {
            DevOpsWorkItemType = AzureDevOpsWorkItemType.Impediment;
        }
    }
}