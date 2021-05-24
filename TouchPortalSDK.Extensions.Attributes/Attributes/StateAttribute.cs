using System;

namespace TouchPortalSDK.Extensions.Attributes.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class StateAttribute : Attribute
    {
        public string Category { get; }

        public StateAttribute(string category)
        {
            Category = category;
        }
    }
}
