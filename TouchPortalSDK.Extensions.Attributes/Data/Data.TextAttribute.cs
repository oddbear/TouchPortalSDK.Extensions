using System;

// ReSharper disable CheckNamespace

namespace TouchPortalSDK.Extensions.Attributes
{
    public static partial class Data
    {
        [AttributeUsage(AttributeTargets.Parameter)]
        public class TextAttribute : DataAttribute
        {
            public override string Type => "text";

            public string Default { get; set; } = string.Empty;
        }
    }
}
