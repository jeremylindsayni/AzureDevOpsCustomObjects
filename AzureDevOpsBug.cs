namespace AzureDevOpsCustomObjects
{
    public class AzureDevOpsBug : AzureDevOpsWorkItem
    {
        public AzureDevOpsBug()
        {
            AzureDevOpsWorkItemType = AzureDevOpsWorkItemType.Bug;
        }
    }
}