using System;
using System.Collections.Generic;
using System.Reflection;
using TouchPortalSDK.Extensions.Attributes;

namespace TouchPortalSDK.Extensions.Reflection.Contexts
{
    public class PluginContext
    {
        public Assembly Assembly { get; }
        public Type Type { get; }
        public PluginAttribute PluginAttribute { get; }

        public IReadOnlyList<SettingContext> SettingContexts { get; }
        public IReadOnlyList<CategoryContext> CategoryContexts { get; }
        public IReadOnlyList<ActionContext> ActionContexts { get; }
        public IReadOnlyList<DataContext> DataContexts { get; }
        public IReadOnlyList<StateContext> StateContexts { get; }
        public IReadOnlyList<EventContext> EventContexts { get; }

        public PluginContext(Assembly assembly, Type type, PluginAttribute pluginAttribute,

                             IReadOnlyList<SettingContext> settingContexts,
                             IReadOnlyList<CategoryContext> categoryContexts,
                             IReadOnlyList<ActionContext> actionContexts,
                             IReadOnlyList<DataContext> dataContexts,
                             IReadOnlyList<StateContext> stateContexts,
                             IReadOnlyList<EventContext> eventContexts)
        {
            Assembly = assembly;
            PluginAttribute = pluginAttribute;
            Type = type;

            SettingContexts = settingContexts ?? throw new ArgumentNullException(nameof(settingContexts));
            CategoryContexts = categoryContexts ?? throw new ArgumentNullException(nameof(categoryContexts));
            ActionContexts = actionContexts ?? throw new ArgumentNullException(nameof(actionContexts));
            DataContexts = dataContexts ?? throw new ArgumentNullException(nameof(dataContexts));
            StateContexts = stateContexts ?? throw new ArgumentNullException(nameof(stateContexts));
            EventContexts = eventContexts ?? throw new ArgumentNullException(nameof(eventContexts));
        }

        public string GetId()
        {
            //Id is set manually:
            if (!string.IsNullOrWhiteSpace(PluginAttribute.Id))
                return PluginAttribute.Id;

            //Remove generics identifier:
            var name = GetClassName();

            //Return namespace + name as id:
            return !string.IsNullOrWhiteSpace(Type.Namespace)
                ? $"{Type.Namespace}.{name}"
                : name;
        }

        public string GetName()
            => !string.IsNullOrWhiteSpace(PluginAttribute.Name)
                ? PluginAttribute.Name
                : GetClassName();

        private string GetClassName()
        {
            //Remove generics identifier:
            var name = Type.Name;
            var index = name.IndexOf('`');
            return index != -1
                ? name.Substring(0, index)
                : name;
        }
    }
}
