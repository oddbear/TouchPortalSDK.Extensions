using System;

// ReSharper disable CheckNamespace

namespace TouchPortalSDK.Extensions.Attributes
{
    public static partial class Data
    {
        [AttributeUsage(AttributeTargets.Parameter)]
        public abstract class DataAttribute : Attribute
        {
            public abstract string Type { get; }

            public string Id { get; set; }
            public string Label { get; set; }
        }
    }
}
