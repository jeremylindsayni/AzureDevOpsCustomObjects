using System;
using AzureDevOpsCustomObjects.Attributes;
using AzureDevOpsCustomObjects.Enumerations;

namespace AzureDevOpsCustomObjects.WorkItems
{
    public class AzureDevOpsTestCase : AzureDevOpsWorkItem
    {
        public AzureDevOpsTestCase()
        {
            DevOpsWorkItemType = AzureDevOpsWorkItemType.TestCase;
        }
        
        [AzureDevOpsPath("/fields/Microsoft.VSTS.Common.ActivatedDate")]
        public DateTime ActivatedDateTime { get; set; }
        
        [AzureDevOpsPath("/fields/Microsoft.VSTS.TCM.Steps")]
        public string TestSteps { get; set; }
        
        [AzureDevOpsPath("/fields/Microsoft.VSTS.TCM.AutomationStatus")]
        public string AutomationStatus { get; set; }
       
        [AzureDevOpsPath("/fields/Microsoft.VSTS.Common.ActivatedBy")]
        public string ActivatedBy { get; set; }

    }
}