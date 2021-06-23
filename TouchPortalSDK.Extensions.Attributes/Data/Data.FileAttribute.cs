using System;

// ReSharper disable CheckNamespace

namespace TouchPortalSDK.Extensions.Attributes
{
    public static partial class Data
    {
        [AttributeUsage(AttributeTargets.Parameter)]
        public class FileAttribute : DataAttribute
        {
            public override string Type => "file";

            public string Default { get; set; } = string.Empty;

            public string[] Extensions { get; set; } = Array.Empty<string>();
        }
    }
}
