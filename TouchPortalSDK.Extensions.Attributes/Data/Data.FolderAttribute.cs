using System;

// ReSharper disable CheckNamespace

namespace TouchPortalSDK.Extensions.Attributes
{
    public static partial class Data
    {
        [AttributeUsage(AttributeTargets.Parameter)]
        public class FolderAttribute : DataAttribute
        {
            public override string Type => "folder";

            public string Default { get; set; } = string.Empty;
        }
    }
}
