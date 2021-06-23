using System.Reflection;
using TouchPortalSDK.Extensions.Attributes;

namespace TouchPortalSDK.Extensions.Reflection.Contexts
{
    public class EventContext
    {
        public CategoryContext CategoryContext { get; }
        public StateContext StateContext { get; }

        public Events.EventAttribute EventAttribute { get; }
        public PropertyInfo PropertyInfo { get; }

        public EventContext(CategoryContext categoryContext, StateContext stateContext, Events.EventAttribute eventAttribute, PropertyInfo propertyInfo)
        {
            CategoryContext = categoryContext;
            StateContext = stateContext;
            EventAttribute = eventAttribute;
            PropertyInfo = propertyInfo;
        }

        public string GetId()
            => !string.IsNullOrWhiteSpace(EventAttribute.Id)
                ? EventAttribute.Id
                : $"{StateContext.CategoryContext.GetId()}.event.{PropertyInfo.Name}";

        public string GetName()
            => !string.IsNullOrWhiteSpace(EventAttribute.Name)
                ? EventAttribute.Name
                : PropertyInfo.Name;

        public string GetValueStateId()
            => !string.IsNullOrWhiteSpace(EventAttribute.ValueStateId)
                ? EventAttribute.ValueStateId
                : StateContext.GetId();
    }
}
