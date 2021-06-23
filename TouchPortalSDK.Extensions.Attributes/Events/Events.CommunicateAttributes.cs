using System;

// ReSharper disable CheckNamespace

namespace TouchPortalSDK.Extensions.Attributes
{
    public static partial class Events
    {
        [AttributeUsage(AttributeTargets.Property)]
        public class CommunicateAttribute : EventAttribute
        {
            public override string Type => "communicate";
        }
    }
}
