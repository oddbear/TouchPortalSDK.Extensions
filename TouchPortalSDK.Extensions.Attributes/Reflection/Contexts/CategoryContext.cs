﻿using System.Reflection;
using TouchPortalSDK.Extensions.Attributes.Attributes;

namespace TouchPortalSDK.Extensions.Attributes.Reflection.Contexts
{
    public class CategoryContext
    {
        public PluginContext PluginContext { get; set; }
        public CategoryAttribute CategoryAttribute { get; }
        public FieldInfo FieldInfo { get; }

        public CategoryContext(PluginContext pluginContext, CategoryAttribute categoryAttribute, FieldInfo fieldInfo)
        {
            PluginContext = pluginContext;
            CategoryAttribute = categoryAttribute;
            FieldInfo = fieldInfo;
        }
        
        public string GetId()
            => !string.IsNullOrWhiteSpace(CategoryAttribute.Id)
                ? CategoryAttribute.Id
                : $"{PluginContext.GetId()}.{FieldInfo.Name}";

        public string GetName()
            => !string.IsNullOrWhiteSpace(CategoryAttribute.Name)
                ? CategoryAttribute.Name
                : FieldInfo.Name;

        public string GetImagePath()
            => CategoryAttribute.ImagePath;
    }
}