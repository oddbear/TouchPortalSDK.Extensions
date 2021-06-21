using System;

namespace TouchPortalSDK.Extensions.Attributes
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Class)]
    public class CategoryAttribute : Attribute
    {
        public string Id { get; }
        public string Name { get; }
        public string ImagePath { get; }

        public CategoryAttribute(string id = null, string name = null, string imagePath = null)
        {
            Id = id;
            Name = name;
            ImagePath = imagePath;
        }
    }
}
