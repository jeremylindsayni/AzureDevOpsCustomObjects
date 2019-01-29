using AzureDevOpsCustomObjects.Attributes;
using AzureDevOpsCustomObjects.Enumerations;

namespace AzureDevOpsCustomObjects.WorkItems
{
    public class AzureDevOpsBug : AzureDevOpsWorkItem
    {
        public AzureDevOpsBug()
        {
            DevOpsWorkItemType = AzureDevOpsWorkItemType.Bug;
        }

        [AzureDevOpsPath("/fields/Microsoft.VSTS.Common.ValueArea")]
        public string ValueArea { get; set; }
        
        [AzureDevOpsPath("/fields/Microsoft.VSTS.TCM.ReproSteps")]
        public string ReproSteps { get; set; }

        [AzureDevOpsPath("/fields/Microsoft.VSTS.Common.AcceptanceCriteria")]
        public string AcceptanceCriteria { get; set; }

        [AzureDevOpsPath("/fields/Microsoft.VSTS.TCM.SystemInfo")]
        public string SystemInformation { get; set; }

        [AzureDevOpsPath("/fields/Microsoft.VSTS.Common.Activity")]
        public string Activity { get; set; }

        [AzureDevOpsPath("/fields/Microsoft.VSTS.Scheduling.Effort")]
        public double? Effort { get; set; }
    }
}