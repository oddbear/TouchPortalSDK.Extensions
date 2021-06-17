using System;

namespace TouchPortalSDK.Extensions.Attributes.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class EventAttribute : Attribute
    {
        public string Id { get; }
        public string Name { get; }
        public string Format { get; }
        public string Type { get; }
        public string[] ValueChoices { get;  }
        public string ValueType { get; }
        public string ValueStateId { get; }

        public EventAttribute(string id = null,
                              string name = null,
                              string format = null,
                              string type = null,
                              string[] valueChoices = null,
                              string valueType = null,
                              string valueStateId = null)
        {
            Id = id;
            Name = name;
            Format = format;
            Type = type ?? "communicate";
            ValueChoices = valueChoices ?? Array.Empty<string>();
            ValueType = valueType ?? "choice";
            ValueStateId = valueStateId;
        }
    }
}