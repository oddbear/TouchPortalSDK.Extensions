using System;

// ReSharper disable CheckNamespace

namespace TouchPortalSDK.Extensions.Attributes
{
    public static partial class Actions
    {
        [AttributeUsage(AttributeTargets.Method)]
        public class CommunicateAttribute : ActionAttribute
        {
            public override string Type => "communicate";

            //TODO: many of the properties should probably be put in here.
        }
    }
}
