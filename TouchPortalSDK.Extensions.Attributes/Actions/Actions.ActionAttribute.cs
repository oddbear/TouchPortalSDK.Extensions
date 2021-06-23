using System;

// ReSharper disable CheckNamespace

namespace TouchPortalSDK.Extensions.Attributes
{
    public static partial class Actions
    {
        [AttributeUsage(AttributeTargets.Method)]
        public abstract class ActionAttribute : Attribute
        {
            public string Category { get; set; } = null;

            public abstract string Type { get; }

            public string Id { get; set; } = null;
            public string Name { get; set; } = null;
            public string Prefix { get; set; } = string.Empty;
            public string Description { get; set; } = null;
            public string Format { get; set; } = null;
            public bool TryInline { get; set; } = true; //Default is false, but false is buggy. So true by default in this implementation.
            public bool HasHoldFunctionality { get; set; } = false;
        }
    }
}
