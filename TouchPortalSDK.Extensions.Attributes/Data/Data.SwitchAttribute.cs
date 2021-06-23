using System;

// ReSharper disable CheckNamespace

namespace TouchPortalSDK.Extensions.Attributes
{
    public static partial class Data
    {
        [AttributeUsage(AttributeTargets.Parameter)]
        public class SwitchAttribute : DataAttribute
        {
            public override string Type => "switch";
            
            public bool Default { get; set; } = false;
        }
    }
}
