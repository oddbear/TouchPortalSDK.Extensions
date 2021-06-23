using System;

// ReSharper disable CheckNamespace

namespace TouchPortalSDK.Extensions.Attributes
{
    public static partial class Events
    {
        [AttributeUsage(AttributeTargets.Property)]
        public abstract class EventAttribute : Attribute
        {
            public abstract string Type { get; }

            public string Id { get; set; }
            public string Name { get; set; }
            public string Format { get; set; } = "$val";
            public string[] ValueChoices { get; set; } = Array.Empty<string>();
            public string ValueType { get; set; } = "choice";
            public string ValueStateId { get; set; }
        }
    }
}
