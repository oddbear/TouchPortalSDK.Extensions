using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using TouchPortalSDK.Extensions.Annotations;
using TouchPortalSDK.Extensions.Reflection;
using TouchPortalSDK.Extensions.Reflection.Contexts;
using TouchPortalSDK.Interfaces;
using TouchPortalSDK.Messages.Events;

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

        public void OnInfoEvent(InfoEvent message)
        {
            //TODO: Implement
        }

        public void OnListChangedEvent(ListChangeEvent message)
        {
            //TODO: Implement
        }

        public void OnBroadcastEvent(BroadcastEvent message)
        {
            //TODO: Implement
        }

        public void OnSettingsEvent(SettingsEvent message)
        {
            //TODO: Implement
        }

        public void OnActionEvent(ActionEvent message)
        {
            var action = _pluginContext.ActionContexts
                .FirstOrDefault(a => a.GetId() == message.ActionId);

            var data = message.Data
                .Select(d => d.Value)
                .ToArray();

            var methodInfo = action?.MethodInfo;
            if (methodInfo is null)
                return;

            //methodInfo.ReturnType.IsAssignableTo(typeof(Task))) <- Could be used before invoke for async run.

            //TODO: Implement other parameters than string, and match ordering (is it always ordered?):
            var returnValue = action?.MethodInfo.Invoke(this, data);

            //TODO: Implement async "support":
            if (returnValue is Task task)
            {
                task.GetAwaiter().GetResult();
            }

        }

        public void OnClosedEvent(string message)
        {
            //TODO: Implement
        }

        public void OnUnhandledEvent(string jsonMessage)
        {
            //TODO: Implement
        }
    }
}
