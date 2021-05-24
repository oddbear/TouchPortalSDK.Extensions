using System;

namespace TouchPortalSDK.Extensions.Attributes.Attributes
{
    [AttributeUsage(AttributeTargets.Field)]
    public class CategoryAttribute : Attribute
    {
        public string Id { get; }
        public string Name { get; }

        public CategoryAttribute(string name, string id = null)
        {
            Id = id;
            Name = name;
        }
    }
}