using System;
using System.Collections.Generic;
using System.Linq;
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

        [AzureDevOpsPath("/fields/System.Id")]
        public int Id { get; set; }

        [AzureDevOpsPath("/fields/System.Title")]
        public string Title { get; set; }

        [AzureDevOpsPath("/fields/System.State")]
        public string State { get; set; }

        [AzureDevOpsPath("/fields/System.CreatedDate")]
        public DateTime? CreatedDate { get; set; }

        [AzureDevOpsPath("/fields/System.ChangedDate")]
        public DateTime? ChangedDate { get; set; }

        [AzureDevOpsPath("/fields/System.CreatedBy")]
        public string CreatedBy { get; set; }

        [AzureDevOpsPath("/fields/System.ChangedBy")]
        public string ChangedBy { get; set; }

        [AzureDevOpsPath("/fields/System.IterationPath")]
        public string Iteration { get; set; }

        [AzureDevOpsPath("/fields/System.AreaPath")]
        public string Area { get; set; }

        [AzureDevOpsPath("/fields/System.TeamProject")]
        public string ProjectName { get; set; }

        [AzureDevOpsPath("/fields/System.Reason")]
        public string Reason { get; set; }

        [AzureDevOpsPath("/fields/Microsoft.VSTS.Common.Priority")]
        public AzureDevOpsWorkItemPriority Priority { get; set; }

        [AzureDevOpsPath("/fields/Microsoft.VSTS.Common.Severity")]
        public AzureDevOpsWorkItemSeverity Severity { get; set; }

        [AzureDevOpsPath("/fields/System.AssignedTo")]
        public string AssignedTo { get; set; }

        [AzureDevOpsPath("/fields/System.Tags")]
        public string Tag { get; set; }

        public IList<AzureDevOpsWorkItemComment> Comments { get; set; }

        public IList<AzureDevOpsWorkItemAttachment> Attachments { get; set; }

        public void Add(JsonPatchOperation pathOperation)
        {
            _jsonPatchDocument.Add(pathOperation);
        }

        public void Remove(string field)
        {
            foreach (var patchOperation in _jsonPatchDocument)
            {
                if (patchOperation.Path != field) continue;
                _jsonPatchDocument.Remove(patchOperation);
                break;
            }
        }

        public void AddOrReplace(string field, object value)
        {
            Remove(field);

            _jsonPatchDocument.Add(
                new JsonPatchOperation
                {
                    Path = field,
                    Value = value
                }
            );
        }

        public JsonPatchDocument ToJsonPatchDocument()
        {
            _jsonPatchDocument.AddRange(
                from property in this.GetType().GetProperties()
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