using System;

namespace TouchPortalSDK.Extensions.Attributes.Attributes
{
    [AttributeUsage(AttributeTargets.Method)]
    public class ActionAttribute : Attribute
    {
        public string Category { get; }

        public string Id { get; }
        public string Name { get; }
        public string Format { get; }

        public ActionAttribute(string category, string id = null, string name = null, string format = null)
        {
            Category = category;

            Id = id;
            Name = name;
            Format = format;
        }
    }
}
