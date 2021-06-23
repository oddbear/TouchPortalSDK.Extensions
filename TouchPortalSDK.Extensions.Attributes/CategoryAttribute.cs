using System;

namespace TouchPortalSDK.Extensions.Attributes
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Class)]
    public class CategoryAttribute : Attribute
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string ImagePath { get; set; }
    }
}
