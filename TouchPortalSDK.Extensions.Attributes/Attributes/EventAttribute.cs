using System;

namespace TouchPortalSDK.Extensions.Attributes.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class EventAttribute : Attribute
    {
        public EventAttribute()
        {
            //TODO: Cannot live without a state. And the state must have a category.
            //TODO: ... we can in theory have them in different categories, but for now, use the state one.

            //valueStateId ...
        }
    }
}