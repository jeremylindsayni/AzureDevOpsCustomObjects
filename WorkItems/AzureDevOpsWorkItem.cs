using AzureDevOpsCustomObjects.Attributes;
using AzureDevOpsCustomObjects.Enumerations;
using Microsoft.VisualStudio.Services.WebApi.Patch;
using Microsoft.VisualStudio.Services.WebApi.Patch.Json;
using System.Linq;

namespace AzureDevOpsCustomObjects.WorkItems
{
    public abstract class AzureDevOpsWorkItem
    {
        private readonly JsonPatchDocument _jsonPatchDocument;

        protected AzureDevOpsWorkItem()
        {
            _jsonPatchDocument = new JsonPatchDocument();
        }

        public AzureDevOpsWorkItemType DevOpsWorkItemType { get; set; }

        public string WorkItemType => DevOpsWorkItemType.GetDescription();

        [AzureDevOpsPath("/fields/System.Title")]
        public string Title { get; set; }

        [AzureDevOpsPath("/fields/System.Description")]
        public string Description { get; set; }

        [AzureDevOpsPath("/fields/Microsoft.VSTS.TCM.ReproSteps")]
        public string ReproSteps { get; set; }

        [AzureDevOpsPath("/fields/Microsoft.VSTS.Common.Priority")]
        public AzureDevOpsWorkItemPriority Priority { get; set; }

        [AzureDevOpsPath("/fields/Microsoft.VSTS.Common.Severity")]
        public AzureDevOpsWorkItemSeverity Severity { get; set; }

        [AzureDevOpsPath("/fields/System.AssignedTo")]
        public string AssignedTo { get; set; }

        [AzureDevOpsPath("/fields/System.History")]
        public string Comment { get; set; }

        [AzureDevOpsPath("/fields/Microsoft.VSTS.Common.Activity")]
        public string Activity { get; set; }

        [AzureDevOpsPath("/fields/Microsoft.VSTS.Common.AcceptanceCriteria")]
        public string AcceptanceCriteria { get; set; }

        [AzureDevOpsPath("/fields/Microsoft.VSTS.TCM.SystemInfo")]
        public string SystemInformation { get; set; }

        [AzureDevOpsPath("/fields/System.Tags")]
        public string Tag { get; set; }

        [AzureDevOpsPath("/fields/Microsoft.VSTS.Scheduling.Effort")]
        public int Effort { get; set; }

        public void Add(JsonPatchOperation pathOperation)
        {
            _jsonPatchDocument.Add(pathOperation);
        }

        public JsonPatchDocument ToJsonPatchDocument()
        {
            _jsonPatchDocument.AddRange(
                from property in typeof(AzureDevOpsWorkItem).GetProperties()
                let attributePath = property.GetFieldPath()
                where attributePath != null

                let propertyValue = property.GetPropertyValue(this)
                where propertyValue != null

                select new JsonPatchOperation
                {
                    Operation = Operation.Add,
                    Path = attributePath,
                    Value = propertyValue
                });

            return _jsonPatchDocument;
        }
    }
}