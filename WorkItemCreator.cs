using System;
using System.IO;
using System.Linq;
using AzureDevOpsCustomObjects.WorkItems;
using Microsoft.TeamFoundation.WorkItemTracking.WebApi;
using Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models;
using Microsoft.VisualStudio.Services.Common;
using Microsoft.VisualStudio.Services.WebApi;
using Microsoft.VisualStudio.Services.WebApi.Patch;
using Microsoft.VisualStudio.Services.WebApi.Patch.Json;

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

            // Add revisions for comments
            foreach (var comment in workItem?.Comments.OrderBy(m => m.OrderingId))
            {
                workItem.AddOrReplace("/fields/System.History", comment.Text);
                Update(workItem);
                workItem.Remove("/fields/System.History");
            }

            foreach (var attachment in workItem?.Attachments.OrderBy(m => m.OrderingId))
                UploadAttachmentToWorkItem(workItem, attachment);

            return workItem.Id;
        }

        private WorkItem Update<T>(T workItem) where T : AzureDevOpsWorkItem
        {
            return WorkItemTrackingHttpClient.UpdateWorkItemAsync(workItem.ToJsonPatchDocument(), workItem.Id).Result;
        }

        private void UploadAttachmentToWorkItem<T>(T workItem, AzureDevOpsWorkItemAttachment attachment)
            where T : AzureDevOpsWorkItem
        {
            var uploadedFileReference = UploadAttachment(attachment);

            PatchWorkItem(workItem, attachment.Comment, uploadedFileReference.Url);

            Update(workItem);
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

        private static void PatchWorkItem<T>(T workItem, string attachmentComment, string uploadedFileUrl)
            where T : AzureDevOpsWorkItem
        {
            const string relationText = "/relations/-";

            workItem.Remove(relationText);
            workItem.Add(new JsonPatchOperation
            {
                Operation = Operation.Add,
                Path = relationText,
                Value = new
                {
                    rel = "AttachedFile",
                    url = uploadedFileUrl,
                    attributes = new
                    {
                        comment = attachmentComment
                    }
                }
            });
        }
    }
}