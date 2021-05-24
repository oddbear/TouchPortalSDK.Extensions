using System;

namespace TouchPortalSDK.Extensions.Attributes.Attributes
{
    [AttributeUsage(AttributeTargets.Class)]
    public class PluginAttribute : Attribute
    {
        public string Id { get; }

        public PluginAttribute(string id = null)
        {
            Id = id;
        }
    }
}
