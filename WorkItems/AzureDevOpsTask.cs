using AzureDevOpsCustomObjects.Attributes;
using AzureDevOpsCustomObjects.Enumerations;

namespace AzureDevOpsCustomObjects.WorkItems
{
    public class AzureDevOpsTask : AzureDevOpsWorkItem
    {
        public AzureDevOpsTask()
        {
            DevOpsWorkItemType = AzureDevOpsWorkItemType.Task;
        }
        
        [AzureDevOpsPath("/fields/Microsoft.VSTS.Scheduling.RemainingWork")]
        public double RemainingWork { get; set; }

        [AzureDevOpsPath("/fields/Microsoft.VSTS.Common.Activity")]
        public string Activity { get; set; }

        [AzureDevOpsPath("/fields/System.Description")]
        public string Description { get; set; }

    }
}