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
            var azureDevOpsWorkItem = InstantiateWorkItem(workItem.Fields["System.WorkItemType"].ToString());

            if (workItem.Id.HasValue)
            {
                azureDevOpsWorkItem.Id = workItem.Id.Value;
            }

            foreach (var field in workItem.Fields)
            {
                var isFieldAssigned = false;
                // assign field values to properties
                foreach (var property in azureDevOpsWorkItem.GetType().GetProperties())
                {
                    var fieldPath = property.GetFieldPath();
                    if ("/fields/" + field.Key != fieldPath)
                        continue;

                    if (!property.PropertyType.IsEnum)
                    {
                        property.SetValue(azureDevOpsWorkItem, field.Value);
                        isFieldAssigned = true;
                    }
                    else
                    {
                        var enumValues = Enum.GetValues(property.PropertyType);
                        foreach (var enumValue in enumValues)
                        {
                            var typedEnumValue = Enum.Parse(property.PropertyType, enumValue.ToString()) as Enum;

                            var description = typedEnumValue.GetDescription();

                            if (field.Value.ToString() != description)
                                continue;

                            property.SetValue(azureDevOpsWorkItem, typedEnumValue);
                            isFieldAssigned = true;
                            break;
                        }
                    }

                    break;
                }

                if (!isFieldAssigned)
                {
                    azureDevOpsWorkItem.AddOrReplace("/fields/" + field.Key, field.Value);
                }
            }

            return azureDevOpsWorkItem;
        }

        private static AzureDevOpsWorkItem InstantiateWorkItem(string workItemType)
        {
            AzureDevOpsWorkItem azureDevOpsWorkItem;

            switch (workItemType)
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
                case "Product Backlog Item":
                    azureDevOpsWorkItem = new AzureDevOpsProductBacklogItem();
                    break;
                case "Task":
                    azureDevOpsWorkItem = new AzureDevOpsTask();
                    break;
                case "Test Case":
                    azureDevOpsWorkItem = new AzureDevOpsTestCase();
                    break;
                default:
                    throw new Exception("Unknown Work Item Type");
            }

            return azureDevOpsWorkItem;
        }
    }
}
