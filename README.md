# Azure DevOps Custom Objects
Experimental package to assist with creation of work items in Azure DevOps boards using .NET.

This project targets the .NET Framework, not .NET Standard.

## Install the package from NuGet:

`Install-Package AzureDevOpsBoardsCustomWorkItemObjects -pre`

## Example use in a .NET Framework project

The code below shows how to add a bug.

```csharp
using AzureDevOpsCustomObjects;

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
                Title = "Spelling mistake on the home page",
                ReproSteps = "Log in, look at the home page - there is a spelling mistake.",
                Priority = AzureDevOpsWorkItemPriority.Medium,
                Severity = AzureDevOpsWorkItemSeverity.Low,
                AssignedTo = "Jeremy Lindsay",
                Comment = "First comment from me",
                Activity = "Development",
                AcceptanceCriteria = "This is the acceptance criteria",
                SystemInformation = "This is the system information",
                Effort = 13,
                Tag = "Cosmetic; UI Only"
            };

            var createdBug = workItemCreator.Create(bug);
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
