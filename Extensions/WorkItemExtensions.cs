using System;
using AzureDevOpsCustomObjects.Attributes;
using AzureDevOpsCustomObjects.WorkItems;
using Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models;

namespace AzureDevOpsCustomObjects.Extensions
{
    public static class WorkItemExtensions
    {
        public static AzureDevOpsWorkItem ToAzureDevOpsWorkItem(this WorkItem workItem)
        {
            AzureDevOpsWorkItem azureDevOpsWorkItem = null;
            switch (workItem.Fields["System.WorkItemType"])
            {
                case "Epic":
                    azureDevOpsWorkItem = new AzureDevOpsEpic();
                    break;
                case "Bug":
                    azureDevOpsWorkItem = new AzureDevOpsBug();
                    break;
                case "Feature":
                    azureDevOpsWorkItem = new AzureDevOpsFeature();
                    break;
                case "Impediment":
                    azureDevOpsWorkItem = new AzureDevOpsImpediment();
                    break;
                case "Issue":
                    azureDevOpsWorkItem = new AzureDevOpsIssue();
                    break;
                case "Product Backlog Item":
                    azureDevOpsWorkItem = new AzureDevOpsProductBacklogItem();
                    break;
                case "Task":
                    azureDevOpsWorkItem = new AzureDevOpsTask();
                    break;
                case "TestCase":
                    azureDevOpsWorkItem = new AzureDevOpsTestCase();
                    break;
                default:
                    throw new Exception("Unknown Work Item Type");
            }

            if (workItem.Id.HasValue)
            {
                azureDevOpsWorkItem.Id = workItem.Id.Value;
            }

            foreach (var field in workItem.Fields)
            {
                // assign field values to properties
                foreach (var property in typeof(AzureDevOpsWorkItem).GetProperties())
                {
                    var fieldPath = property.GetFieldPath();
                    if ("/fields/" + field.Key != fieldPath)
                        continue;

                    if (!property.PropertyType.IsEnum)
                    {
                        property.SetValue(azureDevOpsWorkItem, field.Value);
                    }
                    else
                    {
                        // this is an enum
                        // compare field.Value to enum.Description
                    }

                    break;
                }
            }

            return azureDevOpsWorkItem;
        }
    }
}
