using System;

namespace TouchPortalSDK.Extensions.Attributes.Attributes
{
    [AttributeUsage(AttributeTargets.Method)]
    public class ActionAttribute : Attribute
    {
        public string Category { get; }
        public string Name { get; }

        public ActionAttribute(string category, string name)
        {
            //TODO: Lookup category to check if it's actually real... Give build error if not.
            Category = category;
            Name = name;
        }
    }
}
