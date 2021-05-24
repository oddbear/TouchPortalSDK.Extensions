using System;
using TouchPortalSDK.Messages.Events;

namespace TouchPortalSDK.Extensions
{
    public class TouchPortalPlugin : ITouchPortalEventHandler
    {
        public string PluginId { get; }
        
        public TouchPortalPlugin()
        {
            PluginId = "TODO";
        }

        public void OnInfoEvent(InfoEvent message)
        {
            throw new NotImplementedException();
        }

        public void OnListChangedEvent(ListChangeEvent message)
        {
            throw new NotImplementedException();
        }

        public void OnBroadcastEvent(BroadcastEvent message)
        {
            throw new NotImplementedException();
        }

        public void OnSettingsEvent(SettingsEvent message)
        {
            throw new NotImplementedException();
        }

        public void OnActionEvent(ActionEvent message)
        {
            //var method = _buildEngine.GetActionFromId(message.ActionId);
            //if (method is null)
            //    return;

            throw new NotImplementedException();
        }

        public void OnClosedEvent(string message)
        {
            throw new NotImplementedException();
        }

        public void OnUnhandledEvent(string jsonMessage)
        {
            throw new NotImplementedException();
        }
    }
}
