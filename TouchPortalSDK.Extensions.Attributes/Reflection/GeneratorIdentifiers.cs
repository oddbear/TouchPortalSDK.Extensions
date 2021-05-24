using System;
using System.Collections.Generic;
using System.Reflection;
using TouchPortalSDK.Extensions.Attributes.Attributes;

namespace TouchPortalSDK.Extensions.Attributes.Reflection
{
    public class GeneratorIdentifiers
    {
        private readonly PluginAnalyzer _pluginAnalyzer;

        private readonly Dictionary<Attribute, string> _index = new Dictionary<Attribute, string>();

        public GeneratorIdentifiers(PluginAnalyzer pluginAnalyzer)
        {
            _pluginAnalyzer = pluginAnalyzer;
            BuildIndex();
        }

        private void BuildIndex()
        {
            var (pluginAttribute, type) = _pluginAnalyzer.Plugin;
            var pluginId = GetPluginId(pluginAttribute, type);
            _index[pluginAttribute] = pluginId;

            //TODO: Settings, ActionData etc.

            foreach (var (categoryAttribute, fieldInfo) in _pluginAnalyzer.Categories)
            {
                var categoryId = GetCategoryId(categoryAttribute, fieldInfo);
                var fullCategoryId = $"{pluginId}.{categoryId}";
                _index[categoryAttribute] = fullCategoryId;

                foreach (var (actionAttribute, methodInfo) in _pluginAnalyzer.Actions)
                {
                    var actionId = GetActionId(actionAttribute, methodInfo);
                    var fullActionId = $"{categoryId}.{actionId}";
                    _index[actionAttribute] = fullActionId;
                }

                foreach (var (stateAttribute, propertyInfo) in _pluginAnalyzer.States)
                {
                    var stateId = GetStateId(stateAttribute, propertyInfo);
                    var fullStateId = $"{categoryId}.{stateId}";
                    _index[stateAttribute] = fullStateId;
                }

                foreach (var (eventAttribute, propertyInfo) in _pluginAnalyzer.Events)
                {
                    var eventId = GetEventId(eventAttribute, propertyInfo);
                    var fullEventId = $"{categoryId}.{eventId}";
                    _index[eventAttribute] = fullEventId;
                }
            }
        }

        public string GetIdFromAttribute(Attribute attribute)
        {
            if (_index.ContainsKey(attribute))
                return _index[attribute];

            throw new ArgumentException("Attribute is not indexed.");
        }

        private string GetPluginId(PluginAttribute pluginAttribute, Type type)
        {
            if (!string.IsNullOrWhiteSpace(pluginAttribute.Id))
                return pluginAttribute.Id;

            //Remove generics identifier:
            var name = type.Name;
            var index = name.IndexOf('`');
            name = index != -1
                ? name.Substring(0, index)
                : name;

            return $"{type.Namespace}.{name}";
        }

        private string GetCategoryId(CategoryAttribute categoryAttribute, FieldInfo fieldInfo)
        {
            if (!string.IsNullOrWhiteSpace(categoryAttribute.Id))
                return categoryAttribute.Id;
            
            //TODO: Add namespaces etc.
            return fieldInfo.Name;
        }

        private string GetActionId(ActionAttribute actionAttribute, MethodInfo methodInfo)
        {
            //TODO:
            return "todo:implement";
        }

        private string GetEventId(EventAttribute eventAttribute, PropertyInfo propertyInfo)
        {
            //TODO:
            return "todo:implement";
        }

        private string GetStateId(StateAttribute stateAttribute, PropertyInfo propertyInfo)
        {
            //TODO:
            return "todo:implement";
        }
    }
}
