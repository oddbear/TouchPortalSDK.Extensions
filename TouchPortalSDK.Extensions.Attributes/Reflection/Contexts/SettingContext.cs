using System.Reflection;
using TouchPortalSDK.Extensions.Attributes.Attributes;

namespace TouchPortalSDK.Extensions.Attributes.Reflection.Contexts
{
    public class SettingContext
    {
        public PluginContext PluginContext { get; set; }
        public SettingAttribute SettingAttribute { get; }
        public PropertyInfo PropertyInfo { get; }

        public SettingContext(PluginContext pluginContext, SettingAttribute settingAttribute, PropertyInfo propertyInfo)
        {
            PluginContext = pluginContext;
            SettingAttribute = settingAttribute;
            PropertyInfo = propertyInfo;
        }
        
        public string GetName()
            => !string.IsNullOrWhiteSpace(SettingAttribute.Name)
                ? SettingAttribute.Name
                : PropertyInfo.Name;
    }
}