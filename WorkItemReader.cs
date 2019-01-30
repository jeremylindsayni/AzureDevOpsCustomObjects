using System;
using System.Collections.Generic;
using System.Linq;
using AzureDevOpsCustomObjects.Extensions;
using AzureDevOpsCustomObjects.WorkItems;
using Microsoft.TeamFoundation.WorkItemTracking.WebApi;
using Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models;
using Microsoft.VisualStudio.Services.Common;
using Microsoft.VisualStudio.Services.WebApi;

namespace AzureDevOpsCustomObjects
{
    public class WorkItemReader
    {
        public WorkItemReader(string uri, string personalAccessToken, string projectName)
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

        public IEnumerable<AzureDevOpsWorkItem> All()
        {
            var workItemQuery = new Wiql()
            {
                Query = "Select * " +
                        "From WorkItems " +
                        "Where [System.TeamProject] = '" + ProjectName + "' "
            };

            var workItemQueryResult = WorkItemTrackingHttpClient.QueryByWiqlAsync(workItemQuery).Result;

            if (!workItemQueryResult.WorkItems.Any())
                return new List<AzureDevOpsWorkItem>();

            var matchingWorkItemIds = workItemQueryResult.WorkItems.Select(item => item.Id).ToArray();

            var workItems = WorkItemTrackingHttpClient.GetWorkItemsAsync(ProjectName, matchingWorkItemIds, expand : WorkItemExpand.Relations).Result;

            return workItems.Select(item => item.ToAzureDevOpsWorkItem());
        }
    }
}