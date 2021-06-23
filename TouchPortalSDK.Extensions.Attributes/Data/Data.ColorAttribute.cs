using System;

// ReSharper disable CheckNamespace

namespace TouchPortalSDK.Extensions.Attributes
{
    public static partial class Data
    {
        [AttributeUsage(AttributeTargets.Parameter)]
        public class ColorAttribute : DataAttribute
        {
            public override string Type => "color";

            public string Default { get; set; } = "#00000000";
        }
    }
}
