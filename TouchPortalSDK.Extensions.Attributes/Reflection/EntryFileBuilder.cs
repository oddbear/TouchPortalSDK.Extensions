using System.Collections.Generic;
using System.Linq;

namespace TouchPortalSDK.Extensions.Attributes.Reflection
{
    public class EntryFileBuilder
    {
        private readonly PluginAnalyzer _pluginAnalyzer;
        private readonly GeneratorIdentifiers _generatorIdentifiers;

        public EntryFileBuilder(PluginAnalyzer pluginAnalyzer, GeneratorIdentifiers generatorIdentifiers)
        {
            _pluginAnalyzer = pluginAnalyzer;
            _generatorIdentifiers = generatorIdentifiers;
        }

        public Dictionary<string, object> BuildEntryFile()
        {
            var entry = new Dictionary<string, object>();
            entry["categories"] = BuildCategories();
            return entry;
        }

        private List<object> BuildCategories()
        {
            var categories = new List<object>();
            foreach (var (categoryAttribute, fieldInfo) in _pluginAnalyzer.Categories)
            {
                var category = new Dictionary<string, object>();
                category["name"] = categoryAttribute.Name;
                category["actions"] = BuildActions(categoryAttribute.Name);
                category["states"] = BuildStates(categoryAttribute.Name);
                category["events"] = BuildEvents(categoryAttribute.Name);
                categories.Add(category);
            }

            return categories;
        }

        private List<object> BuildActions(string category)
        {
            var actions = new List<object>();
            foreach (var (actionAttribute, methodInfo) in _pluginAnalyzer.Actions)
            {
                if (actionAttribute.Category != category)
                    continue;

                var action = new Dictionary<string, object>();
                action["id"] = _generatorIdentifiers.GetIdFromAttribute(actionAttribute);
                action["name"] = actionAttribute.Name;
                //TODO: Data
                actions.Add(action);
            }

            return actions;
        }

        private List<object> BuildStates(string category)
        {
            var states = new List<object>();
            foreach (var (stateAttribute, propertyInfo) in _pluginAnalyzer.States)
            {
                if (stateAttribute.Category != category)
                    continue;

                var state = new Dictionary<string, object>();
                state["id"] = _generatorIdentifiers.GetIdFromAttribute(stateAttribute);
                states.Add(state);
            }

            return states;
        }

        private List<object> BuildEvents(string category)
        {
            var events = new List<object>();
            foreach (var (eventAttribute, propertyInfo) in _pluginAnalyzer.Events)
            {
                var (stateAttribute, _) = _pluginAnalyzer.States
                    .SingleOrDefault(state => state.propertyInfo == propertyInfo);

                if (stateAttribute is null)
                    continue; //TODO: Should probably signal that something is wrong here.

                if (stateAttribute.Category != category)
                    continue; //TODO: Might be better with a switch, if not set use state... Otherwise, use events.

                var @event = new Dictionary<string, object>();
                @event["id"] = _generatorIdentifiers.GetIdFromAttribute(eventAttribute);
                @event["valueStateId"] = _generatorIdentifiers.GetIdFromAttribute(stateAttribute);
                events.Add(@event);
            }

            return events;
        }
    }
}
