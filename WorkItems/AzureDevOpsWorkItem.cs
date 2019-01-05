using System;
using System.Reflection;
using AzureDevOpsCustomObjects.Attributes;
using AzureDevOpsCustomObjects.Enumerations;
using Microsoft.VisualStudio.Services.WebApi.Patch;
using Microsoft.VisualStudio.Services.WebApi.Patch.Json;

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

        public void Add(JsonPatchOperation pathOperation)
        {
            _jsonPatchDocument.Add(pathOperation);
        }

        public JsonPatchDocument ToJsonPatchDocument()
        {
            var patchDocument = _jsonPatchDocument;

            var properties = typeof(AzureDevOpsWorkItem).GetProperties();

            foreach (var property in properties)
            {
                var attribute = property.GetCustomAttribute<AzureDevOpsPathAttribute>();

                if (attribute == null) continue;

                var workItemPropertyInfo = typeof(AzureDevOpsWorkItem).GetProperty(property.Name);

                var value = workItemPropertyInfo?.GetValue(this, null);

                if (workItemPropertyInfo?.PropertyType.IsEnum == true)
                {
                    var enumValue = workItemPropertyInfo.GetValue(this, null) as Enum;
                    value = enumValue.GetDescription();
                }

                if (value == null) continue;

                patchDocument.Add(
                    new JsonPatchOperation
                    {
                        Operation = Operation.Add,
                        Path = attribute.Path,
                        Value = value
                    }
                );
            }

            return patchDocument;
        }
    }
}