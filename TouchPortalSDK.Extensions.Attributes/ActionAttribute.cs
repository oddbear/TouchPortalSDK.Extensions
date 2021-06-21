using System;

namespace TouchPortalSDK.Extensions.Attributes
{
    [AttributeUsage(AttributeTargets.Method)]
    public class ActionAttribute : Attribute
    {
        public string Category { get; }

        public string Id { get; }
        public string Name { get; }
        public string Prefix { get; }
        public string Type { get; }
        public string ExecutionType { get; }
        public string ExecutionCmd { get; }
        public string Description { get; }
        public string Format { get; }
        public bool TryInline { get; }
        public bool HasHoldFunctionality { get; }

        public ActionAttribute(string category = null,
                               string id = null,
                               string name = null,
                               string prefix = null,
                               string type = null,
                               string executionType = null,
                               string executionCmd = null,
                               string description = null,
                               bool tryInline = false,
                               string format = null,
                               bool hasHoldFunctionality = false)
        {
            Category = category;

            Id = id;
            Name = name;
            Prefix = prefix ?? string.Empty;
            Type = type ?? "communicate"; //TODO: Use enums?
            ExecutionType = executionType;
            ExecutionCmd = executionCmd;
            Description = description;
            TryInline = tryInline;
            Format = format;
            HasHoldFunctionality = hasHoldFunctionality;
        }
    }
}
