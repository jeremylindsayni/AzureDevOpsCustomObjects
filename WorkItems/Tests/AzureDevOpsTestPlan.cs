using System.Collections.Generic;

namespace AzureDevOpsCustomObjects.WorkItems.Tests
{
    public class AzureDevOpsTestPlan
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public IList<AzureDevOpsTestCase> TestCases { get; set; }
    }
}
