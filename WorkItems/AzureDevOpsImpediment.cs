using AzureDevOpsCustomObjects.Attributes;
using AzureDevOpsCustomObjects.Enumerations;

namespace AzureDevOpsCustomObjects.WorkItems
{
    public class AzureDevOpsImpediment : AzureDevOpsWorkItem
    {
        public AzureDevOpsImpediment()
        {
            DevOpsWorkItemType = AzureDevOpsWorkItemType.Impediment;
        }

        [AzureDevOpsPath("/fields/System.Description")]
        public string Description { get; set; }

    }
}