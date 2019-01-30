using AzureDevOpsCustomObjects.Extensions;
using AzureDevOpsCustomObjects.WorkItems;
using Microsoft.TeamFoundation.WorkItemTracking.WebApi;
using Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models;
using Microsoft.VisualStudio.Services.Common;
using Microsoft.VisualStudio.Services.WebApi;
using Microsoft.VisualStudio.Services.WebApi.Patch;
using Microsoft.VisualStudio.Services.WebApi.Patch.Json;
using System;
using System.IO;
using System.Linq;

namespace AzureDevOpsCustomObjects
{
    public class WorkItemCreator
    {
        public WorkItemCreator(string uri, string personalAccessToken, string projectName)
        {
            Uri = new Uri(uri);
            PersonalAccessToken = personalAccessToken;
            ProjectName = projectName;

            var credentials = new VssBasicCredential(string.Empty, PersonalAccessToken);

            var connection = new VssConnection(Uri, credentials);
            WorkItemTrackingHttpClient = connection.GetClient<WorkItemTrackingHttpClient>();
        }

        private WorkItemTrackingHttpClient WorkItemTrackingHttpClient { get; }

        private Uri Uri { get; }

        private string ProjectName { get; }

        private string PersonalAccessToken { get; }

        public int Create<T>(T workItem) where T : AzureDevOpsWorkItem
        {
            var createdWorkItem = WorkItemTrackingHttpClient
                .CreateWorkItemAsync(workItem.ToJsonPatchDocument(), ProjectName, workItem.WorkItemType).Result;

            workItem.Id = createdWorkItem.Id.Value;

            if (workItem?.Comments != null)
            {
                // Add revisions for comments
                foreach (var comment in workItem?.Comments.OrderBy(m => m.OrderingId))
                {
                    Update(workItem.Id, "/fields/System.History", comment.Text);
                }
            }

            if (workItem?.Attachments != null)
            {
                foreach (var attachment in workItem?.Attachments.OrderBy(m => m.OrderingId))
                {
                    UploadAttachmentToWorkItem(workItem.Id, attachment);
                }
            }

            return workItem.Id;
        }

        public AzureDevOpsWorkItem Update(int workItemId, JsonPatchDocument patchDocument)
        {
            var updatedWorkItem = WorkItemTrackingHttpClient.UpdateWorkItemAsync(patchDocument, workItemId).Result;

            return updatedWorkItem.ToAzureDevOpsWorkItem();
        }

        public AzureDevOpsWorkItem Update(int workItemId, string field, string value)
        {
            var patchDocument = new JsonPatchDocument
            {
                new JsonPatchOperation {Path = field, Value = value}
            };

            var updatedWorkItem = WorkItemTrackingHttpClient.UpdateWorkItemAsync(patchDocument, workItemId).Result;

            return updatedWorkItem.ToAzureDevOpsWorkItem();
        }

        public AzureDevOpsWorkItem UpdateWithParent(int childWorkItemId, int parentWorkItemId)
        {
            const string relationText = "/relations/-";

            var patch = new JsonPatchDocument
            {
                new JsonPatchOperation
                {
                    Operation = Operation.Add,
                    Path = relationText,
                    Value = new
                    {
                        rel = "System.LinkTypes.Hierarchy-Reverse",
                        url = this.Uri + "/" + this.ProjectName + "/_apis/wit/workItems/" + parentWorkItemId
                    }
                }
            };

            var updatedWorkItem = WorkItemTrackingHttpClient.UpdateWorkItemAsync(patch, childWorkItemId).Result;

            return updatedWorkItem.ToAzureDevOpsWorkItem();
        }

        private void UploadAttachmentToWorkItem(int workItemId, AzureDevOpsWorkItemAttachment attachment)
        {
            var uploadedFileReference = UploadAttachment(attachment);
            
            const string relationText = "/relations/-";
            var patch = new JsonPatchDocument
            {
                new JsonPatchOperation
                {
                    Operation = Operation.Add,
                    Path = relationText,
                    Value = new
                    {
                        rel = "AttachedFile",
                        url = uploadedFileReference.Url,
                        attributes = new {comment = attachment.Comment}
                    }
                }
            };

            Update(workItemId, patch);
        }

        private AttachmentReference UploadAttachment(AzureDevOpsWorkItemAttachment attachment)
        {
            AttachmentReference uploadedFile;

            var name = Path.GetFileName(attachment.AttachmentPath);

            using (var attStream = new FileStream(attachment.AttachmentPath, FileMode.Open, FileAccess.Read))
            {
                uploadedFile = WorkItemTrackingHttpClient.CreateAttachmentAsync(attStream, ProjectName, fileName: name)
                    .Result;
            }

            return uploadedFile;
        }
    }
}