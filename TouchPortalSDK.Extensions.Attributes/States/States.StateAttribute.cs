using System;

// ReSharper disable CheckNamespace

namespace TouchPortalSDK.Extensions.Attributes
{
    public static partial class States
    {
        [AttributeUsage(AttributeTargets.Property)]
        public abstract class StateAttribute : Attribute
        {
            public string Category { get; set; }

            public abstract string Type { get; }

            public string Id { get; set; }
            public string Desc { get; set; }
        }
    }
}
