using System;

namespace TouchPortalSDK.Extensions.Attributes.Attributes
{
    [AttributeUsage(AttributeTargets.Class)]
    public class PluginAttribute : Attribute
    {
        public string Id { get; }
        public string Name { get; }

        public PluginAttribute(string id = null, string name = null)
        {
            Id = id;
            Name = name;
        }
    }
}
