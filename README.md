Adapted to use Azure Pipelines, to create a preview version for personal use. Thank you to Jeremy for making such a great module.

[![Build Status](https://dev.azure.com/samsmithnz/Microsoft/_apis/build/status/samsmithnz.AzureDevOpsCustomObjects?branchName=master)](https://dev.azure.com/samsmithnz/Microsoft/_build/latest?definitionId=48&branchName=master)

# Azure DevOps Custom Objects
Experimental package to assist with creation of work items in Azure DevOps boards using .NET.

This project targets the .NET Framework, not .NET Standard.

## Install the package from NuGet:

    Install-Package Microsoft.TeamFoundationServer.Client  
    Install-Package Microsoft.VisualStudio.Services.Client  
    Install-Package AzureDevOpsBoardsCustomWorkItemObjects -pre

## Example use in a .NET Framework project

The code below shows how to add a bug.

```csharp
using System.Collections.Generic;
using AzureDevOpsCustomObjects;
using AzureDevOpsCustomObjects.Enumerations;
using AzureDevOpsCustomObjects.WorkItems;

namespace ConsoleApp
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            const string uri = "https://dev.azure.com/jeremylindsay";
            const string personalAccessToken = "[[***my personal access token***]]";
            const string projectName = "Corvette";

            var workItemCreator = new WorkItemCreator(uri, personalAccessToken, projectName);

            // hardcoded here as an example, but this could be populated any way you want
            // e.g. from a database, or from parsing a CSV
            var bug = new AzureDevOpsBug
            {
                Title = "This is the new title",
                ReproSteps = "Log in.",
                Priority = AzureDevOpsWorkItemPriority.Medium,
                Severity = AzureDevOpsWorkItemSeverity.Low,
                AssignedTo = "Jeremy Lindsay",
                Activity = "Development",
                AcceptanceCriteria = "This is the acceptance criteria",
                SystemInformation = "This is the system information",
                Effort = 13,
                Tag = "Cosmetic; UI Only",
                Comments = new List<AzureDevOpsWorkItemComment>
                {
                    new AzureDevOpsWorkItemComment
                    {
                        OrderingId = 1, Text = "New Comment from product owner."
                    },
                    new AzureDevOpsWorkItemComment
                    {
                        OrderingId = 2, Text = "Another Comment from product owner."
                    }
                },
                Attachments = new List<AzureDevOpsWorkItemAttachment>
                {
                    new AzureDevOpsWorkItemAttachment
                    {
                        OrderingId = 1,
                        AttachmentPath = @"C:\Users\jeremy.lindsay\Desktop\TextFile.txt",
                        Comment = "This is my uploaded text file."
                    },
                    new AzureDevOpsWorkItemAttachment
                    {
                        OrderingId = 2,
                        AttachmentPath = @"C:\Users\jeremy.lindsay\Desktop\ImageFile.png",
                        Comment = "This is my uploaded image file."
                    }
                }
            };

            workItemCreator.Create(bug);
        }
    }
}
```
You can also add any missing fields, as shown in the code below which is an example of how to add a task.
```csharp
using AzureDevOpsCustomObjects;
using Microsoft.VisualStudio.Services.WebApi.Patch.Json;

namespace ConsoleApp
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            const string uri = "https://dev.azure.com/jeremylindsay";
            const string personalAccessToken = "[[***my personal access token***]]";
            const string projectName = "Corvette";

            var workItemCreator = new WorkItemCreator(uri, personalAccessToken, projectName);

            var productBacklogItem = new AzureDevOpsProductBacklogItem
            {
                Title = "Add reports for how many users log in each day",
                Description = "Need a new report with log in statistics.",
                Priority = AzureDevOpsWorkItemPriority.Low,
                Severity = AzureDevOpsWorkItemSeverity.Low,
                AssignedTo = "Jeremy Lindsay",
                Activity = "Development",
                AcceptanceCriteria = "This is the acceptance criteria",
                SystemInformation = "This is the system information",
                Effort = 13,
                Tag = "Reporting; Users"
            };

            productBacklogItem.Add(
                new JsonPatchOperation
                {
                    Path = "/fields/System.History",
                    Value = "Comment from product owner."
                }
            );

            var createdBacklogItem = workItemCreator.Create(productBacklogItem);
        }
    }
}
```
