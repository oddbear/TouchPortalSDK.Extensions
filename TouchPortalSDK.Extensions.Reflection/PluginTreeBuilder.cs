using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using TouchPortalSDK.Extensions.Attributes;
using TouchPortalSDK.Extensions.Reflection.Contexts;

namespace TouchPortalSDK.Extensions.Reflection
{
    public static class PluginTreeBuilder
    {
        public static PluginContext Build(Type pluginType)
        {
            return Build(pluginType.Assembly, pluginType);
        }

        public static PluginContext Build(Assembly assembly, Type pluginType)
        {
            return GetPlugin(assembly, pluginType);

            //Verifications:
            //Action format has all datas?
            //Datas has valid action?
            //All categories are valid?
            //Events has valid states?
            
            //TODO: Wrong place to put this.
            //_pluginTreeVerifier.Verify_Categories(pluginContext);
            //_pluginTreeVerifier.Verify_Event_ValueStateId(pluginContext);

            //return _pluginTreeVerifier.Failure
            //    ? null
            //    : pluginContext;
        }

        public static PluginContext Build(Assembly assembly)
        {
            var allTypes = assembly.GetTypes();
            var pluginType = allTypes
                .Single(type => type.GetCustomAttribute<PluginAttribute>() != null);

            return Build(assembly, pluginType);
        }
        
        private static PluginContext GetPlugin(Assembly assembly, Type pluginType)
        {
            var attribute = pluginType.GetCustomAttribute<PluginAttribute>();

            var settingContexts = new List<SettingContext>();
            var categoryContexts = new List<CategoryContext>();
            var actionContexts = new List<ActionContext>();
            var dataContexts = new List<DataContext>();
            var stateContexts = new List<StateContext>();
            var eventContexts = new List<EventContext>();

            var pluginContext = new PluginContext(assembly, pluginType, attribute, settingContexts, categoryContexts, actionContexts, dataContexts, stateContexts, eventContexts);
            
            //Order matter:
            SetSettings(settingContexts, pluginContext);
            SetCategories(categoryContexts, pluginContext);
            SetActions(actionContexts, pluginContext);
            SetDatas(dataContexts, pluginContext);
            //TODO: Set default action format.
            SetStates(stateContexts, pluginContext);
            SetEvents(eventContexts, pluginContext);

            return pluginContext;
        }

        private static void SetSettings(List<SettingContext> settingContexts, PluginContext pluginContext)
        {
            var settings = pluginContext
                .Type
                .GetProperties()
                .Select(property => new
                {
                    attribute = property.GetCustomAttribute<Settings.SettingAttribute>(),
                    property
                })
                .Where(setting => setting.attribute != null)
                .ToArray();

            foreach (var setting in settings)
            {
                settingContexts.Add(new SettingContext(pluginContext, setting.attribute, setting.property));
            }
        }

        private static void SetCategories(List<CategoryContext> categoryContexts, PluginContext pluginContext)
        {
            //Get category from class:
            var categoriesClassAttribute = pluginContext.Type.GetCustomAttribute<CategoryAttribute>();
            if (categoriesClassAttribute != null)
            {
                categoryContexts.Add(new CategoryContext(pluginContext, categoriesClassAttribute, null));
            }

            //Get categories from Enum:
            var categoriesEnum = pluginContext
                .Type
                .GetNestedTypes()
                .Where(type => type.IsEnum)
                .SelectMany(type => type.GetFields())
                .Select(field => new
                {
                    attribute = field.GetCustomAttribute<CategoryAttribute>(),
                    field
                })
                .Where(category => category.attribute != null)
                .ToArray();

            foreach (var category in categoriesEnum)
            {
                categoryContexts.Add(new CategoryContext(pluginContext, category.attribute, category.field));
            }

            //No categories, create a simple default one:
            if (!categoryContexts.Any())
            {
                categoryContexts.Add(new CategoryContext(pluginContext, new CategoryAttribute(), null));
            }
        }

        private static void SetActions(List<ActionContext> actionContexts, PluginContext plugin)
        {
            var actions = plugin.Type
                .GetMethods()
                .Select(method => new
                {
                    attribute = method.GetCustomAttribute<Actions.ActionAttribute>(),
                    method
                })
                .Where(action => action.attribute != null);

            foreach (var action in actions)
            {
                var categoryContext = plugin.CategoryContexts
                    .SingleOrDefault(category => category.GetName() == action.attribute.Category);

                if (categoryContext is null && plugin.CategoryContexts.Count == 1)
                    categoryContext = plugin.CategoryContexts.Single();
                
                //TODO: How can I generate the Format without any Datas?
                //TODO: And the datas needs a parent to generate the ID. One way is to have a reference to the PluginTree(Context?) in all of them.
                actionContexts.Add(new ActionContext(categoryContext, action.attribute, action.method));
            }
        }

        private static void SetDatas(List<DataContext> dataContexts, PluginContext plugin)
        {
            var datas = plugin.Type
                .GetMethods()
                .SelectMany(method => method.GetParameters().Select(parameter => (parameter, method)))
                .Select(tuple => new {
                    attribute = tuple.parameter.GetCustomAttribute<Data.DataAttribute>(),
                    tuple.parameter,
                    tuple.method
                })
                .Where(action => action.attribute != null)
                .ToArray();

            foreach (var data in datas)
            {
                var actionContext = plugin.ActionContexts
                    .SingleOrDefault(action => action.MethodInfo == data.method);
                
                dataContexts.Add(new DataContext(actionContext, data.attribute, data.method, data.parameter));
            }
        }

        private static void SetStates(List<StateContext> stateContexts, PluginContext plugin)
        {
            var states = plugin.Type
                .GetProperties()
                .Select(property => new
                {
                    attribute = property.GetCustomAttribute<States.StateAttribute>(),
                    property
                })
                .Where(state => state.attribute != null);

            foreach (var state in states)
            {
                var categoryContext = plugin.CategoryContexts
                    .SingleOrDefault(category => category.GetName() == state.attribute.Category);

                if (categoryContext is null && plugin.CategoryContexts.Count == 1)
                    categoryContext = plugin.CategoryContexts.Single();

                stateContexts.Add(new StateContext(categoryContext, state.attribute, state.property));
            }
        }

        private static void SetEvents(List<EventContext> eventContexts, PluginContext plugin)
        {
            var events = plugin.Type
                .GetProperties()
                .Select(property => new
                {
                    attribute = property.GetCustomAttribute<Events.EventAttribute>(),
                    property
                })
                .Where(@event => @event.attribute != null);

            foreach (var @event in events)
            {
                //TODO: Add possibility for manual category?
                var stateContext = plugin.StateContexts
                    .SingleOrDefault(state => state.PropertyInfo == @event.property);

                var categoryContext = stateContext?.CategoryContext;

                eventContexts.Add(new EventContext(categoryContext, stateContext, @event.attribute, @event.property));
            }
        }
    }
}