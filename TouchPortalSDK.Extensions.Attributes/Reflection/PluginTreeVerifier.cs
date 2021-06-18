using System;
using System.Linq;
using Microsoft.Extensions.Logging;
using TouchPortalSDK.Extensions.Attributes.Reflection.Contexts;

namespace TouchPortalSDK.Extensions.Attributes.Reflection
{
    public class PluginTreeVerifier
    {
        private readonly ILogger _logger;
        public bool Failure { get; private set; }

        public PluginTreeVerifier(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory?.CreateLogger<PluginTreeVerifier>();
        }

        public void Verify_Categories(PluginContext pluginContext)
        {
            foreach (var context in pluginContext.ActionContexts)
            {
                var name = context.GetName();
                var category = context.CategoryContext;
                CheckCategory("Action", name, category);
            }

            foreach (var context in pluginContext.StateContexts)
            {
                var name = context.GetDescription();
                var category = context.CategoryContext;
                CheckCategory("State", name, category);
            }

            foreach (var context in pluginContext.EventContexts)
            {
                var desc = context.GetName();
                var category = context.CategoryContext;
                CheckCategory("Event", desc, category);
            }

            void CheckCategory(string type, string name, CategoryContext categoryContext)
            {
                if (categoryContext is null)
                {
                    _logger?.LogError($"{type} '{name}' has an invalid category.");
                    Failure = true;
                }
            }
        }

        public void Verify_Event_ValueStateId(PluginContext pluginContext)
        {
            foreach (var context in pluginContext.EventContexts)
            {
                if (context.StateContext is null)
                {
                    var desc = context.GetName();
                    _logger?.LogError($"Event '{desc}' has an invalid state reference.");
                    Failure = true;
                }
            }
        }

        public void Verify_Action_Format(PluginContext pluginContext)
        {
            foreach (var context in pluginContext.ActionContexts)
            {
                var dataContexts = pluginContext.DataContexts
                    .Where(data => data.ActionContext == context)
                    .ToArray();

                throw new NotImplementedException("Must have children on this one?");
            }
        }

        //TODO: Verify min value < max value.

        //TODO: Verify things like Data.Number -> int, double etc., and Data.Text -> String
    }
}
