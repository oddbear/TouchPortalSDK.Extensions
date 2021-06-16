using System;
using System.Linq;
using System.Reflection;
using TouchPortalSDK.Extensions.Attributes.Attributes;

namespace TouchPortalSDK.Extensions.Attributes.Reflection
{
    public class PluginAnalyzer
    {
        private readonly Type[] _allTypes;

        public (PluginAttribute attribute, Type type) Plugin { get; }
        public (CategoryAttribute attribute, FieldInfo fieldInfo)[] Categories { get; }
        public (ActionAttribute attribute, MethodInfo methodInfo)[] Actions { get; }
        public (StateAttribute attribute, PropertyInfo propertyInfo)[] States { get; }
        public (EventAttribute attribute, PropertyInfo propertyInfo)[] Events { get; }

        public PluginAnalyzer(Assembly assembly)
        {
            _allTypes = assembly.GetTypes();

            Plugin = GetPlugin();
            Categories = GetCategories();
            Actions = GetActions();
            States = GetStates();
            Events = GetEvents();
        }
        
        private (PluginAttribute, Type) GetPlugin()
            => _allTypes
                .Select(@class => (pluginAttribute: @class.GetCustomAttribute<PluginAttribute>(), @class))
                .Single(@class => @class.pluginAttribute != null);

        private (CategoryAttribute, FieldInfo)[] GetCategories()
            => _allTypes
                .Where(type => type.IsEnum)
                .SelectMany(type => type.GetFields())
                .Select(field => (categoryAttribute: field.GetCustomAttribute<CategoryAttribute>(), field))
                .Where(category => category.categoryAttribute != null)
                .ToArray();

        private (ActionAttribute, MethodInfo)[] GetActions()
            => Plugin.type
                .GetMethods()
                .Select(method => (actionAttribute: method.GetCustomAttribute<ActionAttribute>(), method))
                .Where(action => action.actionAttribute != null)
                .ToArray();

        private (StateAttribute, PropertyInfo)[] GetStates()
            => Plugin.type
                .GetProperties()
                .Select(property => (stateAttribute: property.GetCustomAttribute<StateAttribute>(), property))
                .Where(state => state.stateAttribute != null)
                .ToArray();

        private (EventAttribute, PropertyInfo)[] GetEvents()
            => Plugin.type
                .GetProperties()
                .Select(property => (eventAttribute: property.GetCustomAttribute<EventAttribute>(), property))
                .Where(@event => @event.eventAttribute != null)
                .ToArray();
    }
}
