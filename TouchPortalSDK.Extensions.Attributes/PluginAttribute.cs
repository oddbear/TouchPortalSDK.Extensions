using System;

namespace TouchPortalSDK.Extensions.Attributes
{
    [AttributeUsage(AttributeTargets.Class)]
    public class PluginAttribute : Attribute
    {
        public string Id { get; set; }
        public string Name { get; set; }

        public int Version { get; set; } = 1;
        public int Sdk { get; set; } = 3;

        public string ColorDark { get; set; }
        public string ColorLight { get; set; }
    }
}
