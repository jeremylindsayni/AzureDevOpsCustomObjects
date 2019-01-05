using System;

namespace AzureDevOpsCustomObjects
{
    [AttributeUsage(AttributeTargets.Property)]
    public class AzureDevOpsPathAttribute : Attribute
    {
        public AzureDevOpsPathAttribute(string path)
        {
            Path = path;
        }

        public string Path { get; }
    }
}