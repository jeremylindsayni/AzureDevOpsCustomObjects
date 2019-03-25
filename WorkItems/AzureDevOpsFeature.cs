using AzureDevOpsCustomObjects.Attributes;
using AzureDevOpsCustomObjects.Enumerations;

namespace AzureDevOpsCustomObjects.WorkItems
{
    public class AzureDevOpsFeature : AzureDevOpsWorkItem
    {
        public AzureDevOpsFeature()
        {
            DevOpsWorkItemType = AzureDevOpsWorkItemType.Feature;
        }

        [AzureDevOpsPath("/fields/Microsoft.VSTS.Common.ValueArea")]
        public string ValueArea { get; set; }

        [AzureDevOpsPath("/fields/Microsoft.VSTS.Common.BusinessValue")]
        public long BusinessValue { get; set; }

        [AzureDevOpsPath("/fields/Microsoft.VSTS.Common.TimeCriticality")]
        public double TimeCriticality { get; set; }

        [AzureDevOpsPath("/fields/Microsoft.VSTS.Common.AcceptanceCriteria")]
        public string AcceptanceCriteria { get; set; }

        [AzureDevOpsPath("/fields/System.Description")]
        public string Description { get; set; }

        [AzureDevOpsPath("/fields/Microsoft.VSTS.Scheduling.Effort")]
        public double? Effort { get; set; }

        [AzureDevOpsPath("/fields/Microsoft.VSTS.Scheduling.TargetDate")]
        public System.DateTime TargetDate { get; set; }
    }
}