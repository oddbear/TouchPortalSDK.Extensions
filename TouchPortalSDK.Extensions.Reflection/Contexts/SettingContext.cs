using System.Reflection;
using TouchPortalSDK.Extensions.Attributes;

namespace TouchPortalSDK.Extensions.Reflection.Contexts
{
    public class SettingContext
    {
        public PluginContext PluginContext { get; set; }
        public Settings.SettingAttribute SettingAttribute { get; }
        public PropertyInfo PropertyInfo { get; }

        public SettingContext(PluginContext pluginContext, Settings.SettingAttribute settingAttribute, PropertyInfo propertyInfo)
        {
            PluginContext = pluginContext;
            SettingAttribute = settingAttribute;
            PropertyInfo = propertyInfo;
        }
        
        public string GetName()
            => !string.IsNullOrWhiteSpace(SettingAttribute.Name)
                ? SettingAttribute.Name
                : PropertyInfo.Name;

        public string GetSettingType() //GetType is a reserved method name.
            => !string.IsNullOrWhiteSpace(SettingAttribute.Type)
                ? SettingAttribute.Type
                : GetTypeFromProperty();

        //TODO: Maybe make two different attributes. One for text and one for number (ex. min/max value does not make sense for text).
        private string GetTypeFromProperty()
            => PropertyInfo.PropertyType == typeof(string)
                ? "text"
                : "number"; //Ex. int, long, double, float, decimal
    }
}