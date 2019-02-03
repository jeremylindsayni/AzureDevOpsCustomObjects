using System.Collections.Generic;

namespace AzureDevOpsCustomObjects.WorkItems.Tests
{
    public class AzureDevOpsTestCase
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public IList<AzureDevOpsTestStep> TestSteps { get; set; }
    }
}