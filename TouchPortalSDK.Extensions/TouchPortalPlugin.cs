﻿using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using TouchPortalSDK.Extensions.Annotations;
using TouchPortalSDK.Messages.Events;

namespace TouchPortalSDK.Extensions
{
    public class TouchPortalPlugin : INotifyPropertyChanged, ITouchPortalEventHandler
    {
        public string PluginId { get; }
        
        public TouchPortalPlugin()
        {
            PluginId = "TODO";
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
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
