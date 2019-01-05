using System;
using AzureDevOpsCustomObjects.WorkItems;
using Microsoft.TeamFoundation.WorkItemTracking.WebApi;
using Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models;
using Microsoft.VisualStudio.Services.Common;
using Microsoft.VisualStudio.Services.WebApi;

namespace AzureDevOpsCustomObjects
{
    public class WorkItemCreator
    {
        public WorkItemCreator(string uri, string personalAccessToken, string projectName)
        {
            Uri = new Uri(uri);
            PersonalAccessToken = personalAccessToken;
            ProjectName = projectName;
        }

        private Uri Uri { get; }

        private string ProjectName { get; }

        private string PersonalAccessToken { get; }

        public WorkItem Create<T>(T bug) where T : AzureDevOpsWorkItem
        {
            var credentials = new VssBasicCredential(string.Empty, PersonalAccessToken);

            var connection = new VssConnection(Uri, credentials);
            var workItemTrackingHttpClient = connection.GetClient<WorkItemTrackingHttpClient>();

            return workItemTrackingHttpClient
                .CreateWorkItemAsync(bug.ToJsonPatchDocument(), ProjectName, bug.WorkItemType).Result;
        }
    }
}