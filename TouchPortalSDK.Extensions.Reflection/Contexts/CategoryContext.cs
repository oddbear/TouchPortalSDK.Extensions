using System.Reflection;
using TouchPortalSDK.Extensions.Attributes;

namespace TouchPortalSDK.Extensions.Reflection.Contexts
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
                : $"{PluginContext.GetId()}.{GetCategoryId()}";

        public string GetName()
            => !string.IsNullOrWhiteSpace(CategoryAttribute.Name)
                ? CategoryAttribute.Name
                : FieldInfo?.Name ?? PluginContext.GetName();

        private string GetCategoryId()
            => FieldInfo?.Name ?? "DefaultCategory";
    }
}
