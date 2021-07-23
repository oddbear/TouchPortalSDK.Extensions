using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using TouchPortalSDK.Extensions.Annotations;
using TouchPortalSDK.Extensions.Attributes;
using TouchPortalSDK.Extensions.Reflection;
using TouchPortalSDK.Extensions.Reflection.Contexts;
using TouchPortalSDK.Interfaces;
using TouchPortalSDK.Messages.Events;
using TouchPortalSDK.Messages.Models;

namespace TouchPortalSDK.Extensions
{
    public class TouchPortalPlugin : INotifyPropertyChanged, ITouchPortalEventHandler
    {
        public string PluginId { get; }

        private readonly ITouchPortalClient _touchPortalClient;
        private readonly PluginContext _pluginContext;
        
        public TouchPortalPlugin(TouchPortalOptions options = null, ILoggerFactory loggerFactory = null)
        {
            _pluginContext = PluginTreeBuilder.Build(this.GetType());
            PluginId = _pluginContext.GetId();

            _touchPortalClient = TouchPortalFactory.CreateClient(this, options, loggerFactory);
            _touchPortalClient.Connect();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private object StringToValue(Type output, string value)
        {
            if (output == typeof(string))
                return value;

            try
            {
                //TODO: Convert to switch/parse:
                return JsonSerializer.Deserialize(value, output);
            }
            catch (JsonException)
            {
                return Activator.CreateInstance(output);
            }
        }

        private void SetSettings(IEnumerable<Setting> settings)
        {
            foreach (var setting in settings)
            {
                var settingContext = _pluginContext.SettingContexts
                    .FirstOrDefault(s => s.GetName() == setting.Name);

                if (settingContext is null)
                    continue;

                var propertyValue = settingContext.PropertyInfo.GetValue(this);
                var messageValue = StringToValue(settingContext.PropertyInfo.PropertyType, setting.Value);
                if (propertyValue != messageValue && settingContext.PropertyInfo.SetMethod != null)
                {
                    settingContext.PropertyInfo.SetValue(this, messageValue);
                }
            }
        }

        public void OnInfoEvent(InfoEvent message)
        {
            SetSettings(message.Settings);
        }

        public void OnListChangedEvent(ListChangeEvent message)
        {
            //TODO: Implement
        }

        public virtual void OnBroadcastEvent(BroadcastEvent message)
        {
            //TODO: Implement
        }

        public void OnSettingsEvent(SettingsEvent message)
        {
            SetSettings(message.Values);
        }

        public void OnActionEvent(ActionEvent message)
        {
            var action = _pluginContext.ActionContexts
                .FirstOrDefault(a => a.GetId() == message.ActionId);
            
            var methodInfo = action?.MethodInfo;
            if (methodInfo is null)
                return;
            
            //TODO: Model? Reuse code?
            var parameters = methodInfo.GetParameters();
            var arguments = new object[parameters.Length];
            foreach (var parameterInfo in parameters)
            {
                var attribute = parameterInfo.GetCustomAttribute<Data.DataAttribute>();
                if (attribute is null)
                    continue;

                var dataContext = _pluginContext.DataContexts
                    .FirstOrDefault(context => context.ParameterInfo == parameterInfo);

                if (dataContext is null)
                    continue;

                var value = message.Data
                    .FirstOrDefault(dataSelected => dataSelected.Id == dataContext.GetId())
                    ?.Value;

                if (value is null)
                    continue;

                arguments[parameterInfo.Position] = StringToValue(parameterInfo.ParameterType, value);
            }
            
            //Invoke the Action Method:
            _ = typeof(Task).IsAssignableFrom(methodInfo.ReturnType)
                ? Task.Run(() => (Task)action?.MethodInfo.Invoke(this, arguments) ?? Task.CompletedTask)
                : action?.MethodInfo.Invoke(this, arguments);
        }

        public virtual void OnClosedEvent(string message)
        {
            //TODO: Implement
        }

        public virtual void OnUnhandledEvent(string jsonMessage)
        {
            //TODO: Implement
        }
    }
}
