using System;
using System.Collections.Generic;
using TouchPortalSDK.Extensions.Attributes.Reflection.Contexts;

namespace TouchPortalSDK.Extensions.Attributes.Reflection
{
    public static class EntryFileBuilder
    {
        public static Dictionary<string, object> BuildEntryFile(PluginContext pluginContext)
        {
            var entry = new Dictionary<string, object>();
            //TODO: Configuration...
            //TODO: Plugin_start_cmd...
            //TODO: Settings...
            entry["sdk"] = 3;
            entry["version"] = 1; //TODO: Fix
            entry["id"] = pluginContext.GetId();
            entry["name"] = pluginContext.GetName();
            entry["settings"] = BuildSettings(pluginContext);
            entry["categories"] = BuildCategories(pluginContext);
            return entry;
        }

        private static List<object> BuildSettings(PluginContext pluginContext)
        {
            var categories = new List<object>();
            foreach (var categoryContext in pluginContext.SettingContexts)
            {
                var category = new Dictionary<string, object>();
                category["name"] = categoryContext.GetName();
                categories.Add(category);
            }

            return categories;
        }

        private static List<object> BuildCategories(PluginContext pluginContext)
        {
            var categories = new List<object>();
            foreach (var categoryContext in pluginContext.CategoryContexts)
            {
                var category = new Dictionary<string, object>();
                category["id"] = categoryContext.GetId();
                category["name"] = categoryContext.GetName();
                if (categoryContext.GetImagePath() != null)
                    category["imagepath"] = categoryContext.GetImagePath();
                category["actions"] = BuildActions(pluginContext, categoryContext);
                category["states"] = BuildStates(pluginContext, categoryContext);
                category["events"] = BuildEvents(pluginContext, categoryContext);
                categories.Add(category);
            }

            return categories;
        }

        private static List<object> BuildActions(PluginContext pluginContext, CategoryContext categoryContext)
        {
            var actions = new List<object>();
            foreach (var actionContext in pluginContext.ActionContexts)
            {
                if (actionContext.CategoryContext != categoryContext)
                    continue;

                var action = new Dictionary<string, object>();
                action["id"] = actionContext.GetId();
                action["name"] = actionContext.GetName();
                //"prefix": "prefix",
                //"type": "communicate",
                //"description": "Description",
                //"format": "Format {$dataId$}",
                //"tryInline": true,
                //"hasHoldFunctionality": false
                //TODO: Data
                action["data"] = BuildData(pluginContext, actionContext);
                actions.Add(action);
            }

            return actions;
        }

        private static List<object> BuildData(PluginContext pluginContext, ActionContext actionContext)
        {
            var datas = new List<object>();
            foreach (var dataContext in pluginContext.DataContexts)
            {
                if (dataContext.ActionContext != actionContext)
                    continue;

                var data = new Dictionary<string, object>();
                data["id"] = dataContext.GetId();
                data["label"] = dataContext.GetLabel();
                //"type": "text",
                //"default": ""
                //"valueChoices": ["1", "2", "3"]

                datas.Add(data);
            }
            return datas;
        }

        private static List<object> BuildStates(PluginContext pluginContext, CategoryContext categoryContext)
        {
            var states = new List<object>();
            foreach (var stateContext in pluginContext.StateContexts)
            {
                if (stateContext.CategoryContext != categoryContext)
                    continue;

                var state = new Dictionary<string, object>();
                state["id"] = stateContext.GetId();
                state["name"] = stateContext.GetDescription();
                states.Add(state);
            }

            return states;
        }

        private static List<object> BuildEvents(PluginContext pluginContext, CategoryContext categoryContext)
        {
            var events = new List<object>();
            foreach (var eventContext in pluginContext.EventContexts)
            {
                //TODO: Possibility to manually set category?
                if (eventContext.StateContext.CategoryContext != categoryContext)
                    continue;

                var @event = new Dictionary<string, object>();
                @event["id"] = eventContext.GetId();
                @event["desc"] = eventContext.GetName();
                @event["valueStateId"] = eventContext.GetValueStateId();
                events.Add(@event);
            }

            return events;
        }
    }
}
