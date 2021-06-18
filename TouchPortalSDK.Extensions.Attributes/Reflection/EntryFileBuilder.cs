using System;
using System.Collections.Generic;
using System.Linq;
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

            //Required:
            entry["sdk"] = 3;

            //Required:
            entry["version"] = 1; //TODO: Fix

            //Required:
            entry["name"] = pluginContext.GetName();

            //Required:
            entry["id"] = pluginContext.GetId();

            //Optional:
            //entry["configuration"] = 

            //Optional:
            //entry["plugin_start_cmd"] = 

            //Required:
            entry["categories"] = BuildCategories(pluginContext);

            //Optional:
            entry["settings"] = BuildSettings(pluginContext);

            return entry;
        }

        private static List<object> BuildSettings(PluginContext pluginContext)
        {
            var settings = new List<object>();

            foreach (var settingContext in pluginContext.SettingContexts)
            {
                var setting = new Dictionary<string, object>();

                var attribute = settingContext.SettingAttribute;

                //Required:
                setting["name"] = settingContext.GetName();

                //Optional:
                //setting["default"] = 

                //Required:
                setting["type"] = settingContext.GetSettingType();

                //Optional:
                //setting["maxLenght"] = // <- Is this typo thing fixed?

                //Optional:
                setting["isPassword"] = attribute.IsPassword;

                //Optional:
                //setting["minValue"] = 

                //Optional:
                //setting["maxValue"] = 

                //Optional:
                setting["readOnly"] = attribute.ReadOnly;

                settings.Add(setting);
            }

            return settings;
        }

        private static List<object> BuildCategories(PluginContext pluginContext)
        {
            var categories = new List<object>();

            foreach (var categoryContext in pluginContext.CategoryContexts)
            {
                var category = new Dictionary<string, object>();

                var attribute = categoryContext.CategoryAttribute;

                //Required:
                category["id"] = categoryContext.GetId();

                //Required:
                category["name"] = categoryContext.GetName();

                //Optional:
                if (!string.IsNullOrWhiteSpace(attribute.ImagePath))
                    category["imagepath"] = attribute.ImagePath;

                //Required (but can be empty):
                category["actions"] = BuildActions(pluginContext, categoryContext);

                //Required (but can be empty):
                category["states"] = BuildStates(pluginContext, categoryContext);

                //Required (but can be empty):
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

                var attribute = actionContext.ActionAttribute;

                var action = new Dictionary<string, object>();

                //Required:
                action["id"] = actionContext.GetId();

                //Required:
                action["name"] = actionContext.GetName();

                //Required:
                action["prefix"] = attribute.Prefix;

                //Required:
                action["type"] = attribute.Type;

                //Optional:
                if (!string.IsNullOrWhiteSpace(attribute.ExecutionType))
                    action["executionType"] = attribute.ExecutionType;

                //Optional:
                if (!string.IsNullOrWhiteSpace(attribute.ExecutionCmd))
                    action["execution_cmd"] = attribute.ExecutionCmd;

                //Optional:
                if (!string.IsNullOrWhiteSpace(attribute.Description))
                    action["description"] = attribute.Description;

                //Optional:
                //TODO: If it does not exist, it will only show the (Prefix|Name) both inlined or not.
                //For now the rule should be to not include this if empty.
                //In the future, generate a default if empty?
                if (!string.IsNullOrWhiteSpace(attribute.Format))
                    action["format"] = attribute.Format;

                //Optional:
                action["tryInline"] = attribute.TryInline;

                //Optional:
                action["hasHoldFunctionality"] = attribute.HasHoldFunctionality;

                //Optional:
                if (pluginContext.DataContexts.Any())
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
