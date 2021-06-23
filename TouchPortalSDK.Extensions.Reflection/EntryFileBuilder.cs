using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using TouchPortalSDK.Extensions.Attributes;
using TouchPortalSDK.Extensions.Reflection.Contexts;

namespace TouchPortalSDK.Extensions.Reflection
{
    public static class EntryFileBuilder
    {
        public static Dictionary<string, object> BuildEntryFile(PluginContext pluginContext)
        {
            var entryObject = new Dictionary<string, object>();

            var pluginAttribute = pluginContext.PluginAttribute;

            //Required:
            entryObject["sdk"] = pluginAttribute.Sdk;

            //Required:
            entryObject["version"] = pluginAttribute.Version;

            //Required:
            entryObject["name"] = pluginContext.GetName();

            //Required:
            entryObject["id"] = pluginContext.GetId();

            //Optional:
            var configurationObject = BuildConfiguration(pluginAttribute);
            if (configurationObject.Any())
            {
                entryObject["configuration"] = configurationObject;
            }

            //TODO: Plugin_start_cmd...
            //Optional:
            //entry["plugin_start_cmd"] = 

            //Required:
            entryObject["categories"] = BuildCategories(pluginContext);

            //Optional:
            entryObject["settings"] = BuildSettings(pluginContext);

            return entryObject;
        }

        private static Dictionary<string, object> BuildConfiguration(PluginAttribute pluginAttribute)
        {
            var configurationObject = new Dictionary<string, object>();
            if (pluginAttribute.ColorDark != null)
            {
                configurationObject["colorDark"] = pluginAttribute.ColorDark;
            }

            if (pluginAttribute.ColorDark != null)
            {
                configurationObject["colorLight"] = pluginAttribute.ColorLight;
            }

            return configurationObject;
        }

        private static List<object> BuildSettings(PluginContext pluginContext)
        {
            var settings = new List<object>();

            foreach (var settingContext in pluginContext.SettingContexts)
            {
                var settingObject = new Dictionary<string, object>();

                var settingAttribute = settingContext.SettingAttribute;

                //Required:
                settingObject["name"] = settingContext.GetName();
                
                //Required:
                settingObject["type"] = settingContext.GetSettingType();

                if (settingAttribute is Settings.TextAttribute textAttribute)
                {
                    //Optional:
                    if (textAttribute.Default != null)
                    {
                        settingObject["default"] = textAttribute.Default;
                    }

                    //Optional:
                    if (textAttribute.MaxLength != int.MinValue)
                    {
                        settingObject["maxLenght"] = textAttribute.MaxLength; //TODO: Is this typo fixed, or is it permanent (still in the documentation).
                    }
                }

                //Optional:
                settingObject["isPassword"] = settingAttribute.IsPassword;

                if (settingAttribute is Settings.NumberAttribute numberAttribute)
                {
                    //Optional:
                    if (!double.IsNaN(numberAttribute.Default))
                    {
                        settingObject["default"] = numberAttribute.Default.ToString(CultureInfo.InvariantCulture);
                    }

                    if (!double.IsNaN(numberAttribute.MinValue))
                    {
                        //Optional:
                        settingObject["minValue"] = numberAttribute.MinValue;
                    }

                    if (!double.IsNaN(numberAttribute.MaxValue))
                    {
                        //Optional:
                        settingObject["maxValue"] = numberAttribute.MaxValue;
                    }
                }

                //Optional:
                settingObject["readOnly"] = settingAttribute.ReadOnly;

                settings.Add(settingObject);
            }

            return settings;
        }

        private static List<object> BuildCategories(PluginContext pluginContext)
        {
            var categories = new List<object>();

            foreach (var categoryContext in pluginContext.CategoryContexts)
            {
                var categoryObject = new Dictionary<string, object>();

                var categoryAttribute = categoryContext.CategoryAttribute;

                //Required:
                categoryObject["id"] = categoryContext.GetId();

                //Required:
                categoryObject["name"] = categoryContext.GetName();

                //Optional:
                if (!string.IsNullOrWhiteSpace(categoryAttribute.ImagePath))
                    categoryObject["imagepath"] = categoryAttribute.ImagePath;

                //Required (but can be empty):
                categoryObject["actions"] = BuildActions(pluginContext, categoryContext);

                //Required (but can be empty):
                categoryObject["states"] = BuildStates(pluginContext, categoryContext);

                //Required (but can be empty):
                categoryObject["events"] = BuildEvents(pluginContext, categoryContext);

                categories.Add(categoryObject);
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

                var actionAttribute = actionContext.ActionAttribute;

                var actionObject = new Dictionary<string, object>();

                //Required:
                actionObject["id"] = actionContext.GetId();

                //Required:
                actionObject["name"] = actionContext.GetName();

                //Required:
                actionObject["prefix"] = actionAttribute.Prefix;

                //Required:
                actionObject["type"] = actionAttribute.Type;

                if (actionAttribute is Actions.ExecuteAttribute executeAttribute)
                {
                    //Optional:
                    if (!string.IsNullOrWhiteSpace(executeAttribute.ExecutionType))
                        actionObject["executionType"] = executeAttribute.ExecutionType;

                    //Optional:
                    if (!string.IsNullOrWhiteSpace(executeAttribute.ExecutionCmd))
                        actionObject["execution_cmd"] = executeAttribute.ExecutionCmd;
                }

                //Optional:
                if (!string.IsNullOrWhiteSpace(actionAttribute.Description))
                    actionObject["description"] = actionAttribute.Description;

                //Optional:
                //TODO: If it does not exist, it will only show the (Prefix|Name) both inlined or not.
                //For now the rule should be to not include this if empty.
                //In the future, generate a default if empty?
                if (!string.IsNullOrWhiteSpace(actionAttribute.Format))
                    actionObject["format"] = actionAttribute.Format;

                //Optional:
                actionObject["tryInline"] = actionAttribute.TryInline;

                //Optional:
                actionObject["hasHoldFunctionality"] = actionAttribute.HasHoldFunctionality;

                //Optional:
                if (pluginContext.DataContexts.Any())
                    actionObject["data"] = BuildData(pluginContext, actionContext);

                actions.Add(actionObject);
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

                var dataAttribute = dataContext.DataAttribute;

                var dataObject = new Dictionary<string, object>();

                //Required:
                dataObject["id"] = dataContext.GetId();

                //Required:
                dataObject["type"] = dataAttribute.Type;

                //Required:
                dataObject["label"] = dataContext.GetLabel();

                if (dataAttribute is Data.TextAttribute textAttribute)
                {
                    //Required:
                    dataObject["default"] = textAttribute.Default;
                }

                if (dataAttribute is Data.FolderAttribute folderAttribute)
                {
                    //Required:
                    dataObject["default"] = folderAttribute.Default;
                }

                if (dataAttribute is Data.SwitchAttribute switchAttribute)
                {
                    //Required:
                    dataObject["default"] = switchAttribute.Default;
                }

                if (dataAttribute is Data.ColorAttribute colorAttribute)
                {
                    //Required:
                    dataObject["default"] = colorAttribute.Default;
                }

                if (dataAttribute is Data.ChoiceAttribute choiceAttribute)
                {
                    //Required:
                    dataObject["default"] = choiceAttribute.Default;

                    //Required (if choice):
                    dataObject["valueChoices"] = choiceAttribute.ValueChoices;
                }

                if (dataAttribute is Data.FileAttribute fileAttribute)
                {
                    //Required:
                    dataObject["default"] = fileAttribute.Default;

                    //Optional:
                    if (fileAttribute.Extensions.Any())
                    {
                        dataObject["extensions"] = fileAttribute.Extensions;
                    }
                }

                if (dataAttribute is Data.NumberAttribute numberAttribute)
                {
                    //Required:
                    dataObject["default"] = numberAttribute.Default;

                    //Optional:
                    dataObject["allowDecimals"] = numberAttribute.AllowDecimals;

                    //Optional:
                    if (!double.IsNaN(numberAttribute.MinValue))
                    {
                        dataObject["minValue"] = numberAttribute.MinValue;
                    }

                    //Optional:
                    if (!double.IsNaN(numberAttribute.MaxValue))
                    {
                        dataObject["maxValue"] = numberAttribute.MaxValue;
                    }
                }

                datas.Add(dataObject);
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

                var stateAttribute = stateContext.StateAttribute;

                var stateObject = new Dictionary<string, object>();

                //Required:
                stateObject["id"] = stateContext.GetId();

                //Required:
                stateObject["type"] = stateAttribute.Type;

                //Required:
                stateObject["desc"] = stateContext.GetDescription();

                if (stateAttribute is States.ChoiceAttribute choiceAttribute)
                {
                    //Required:
                    stateObject["default"] = choiceAttribute.Default;

                    //Optional:
                    if (choiceAttribute.ValueChoices != null)
                        stateObject["valueChoices"] = choiceAttribute.ValueChoices;
                }

                if (stateAttribute is States.TextAttribute textAttribute)
                {
                    //Required:
                    stateObject["default"] = textAttribute.Default;
                }

                states.Add(stateObject);
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

                var eventAttribute = eventContext.EventAttribute;

                var eventObject = new Dictionary<string, object>();

                //Required:
                eventObject["id"] = eventContext.GetId();

                //Required:
                eventObject["name"] = eventContext.GetName();

                //Required:
                eventObject["format"] = eventAttribute.Format;

                //Required:
                eventObject["type"] = eventAttribute.Type;

                //Required:
                eventObject["valueChoices"] = eventAttribute.ValueChoices;

                //Required:
                eventObject["valueType"] = eventAttribute.ValueType;

                //Required:
                eventObject["valueStateId"] = eventContext.GetValueStateId();

                events.Add(eventObject);
            }

            return events;
        }
    }
}
