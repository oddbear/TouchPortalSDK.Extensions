using System;

namespace TouchPortalSDK.Extensions.Attributes.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class StateAttribute : Attribute
    {
        public string Category { get; }

        public string Id { get; }
        public string Type { get; }
        public string Desc { get; }
        public string Default { get; }
        public string[] ValueChoices { get; }

        public StateAttribute(string category,
                              string id = null,
                              string type = null,
                              string desc = null,
                              string @default = null,
                              string[] valueChoices = null)
        {
            Category = category;

            Id = id;
            Type = type; //choice, text in format: #FF115599 or base64 image.
            Desc = desc;
            Default = @default;
            ValueChoices = valueChoices ?? Array.Empty<string>();
        }
    }
}
