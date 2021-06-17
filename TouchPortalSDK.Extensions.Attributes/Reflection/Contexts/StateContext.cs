using System.Reflection;
using TouchPortalSDK.Extensions.Attributes.Attributes;

namespace TouchPortalSDK.Extensions.Attributes.Reflection.Contexts
{
    public class StateContext
    {
        public CategoryContext CategoryContext { get; }

        public StateAttribute StateAttribute { get; }
        public PropertyInfo PropertyInfo { get; }

        public StateContext(CategoryContext categoryContext, StateAttribute stateAttribute, PropertyInfo propertyInfo)
        {
            CategoryContext = categoryContext;
            StateAttribute = stateAttribute;
            PropertyInfo = propertyInfo;
        }

        public string GetId()
            => !string.IsNullOrWhiteSpace(StateAttribute.Id)
                ? StateAttribute.Id
                : $"{CategoryContext.GetId()}.state.{PropertyInfo.Name}";

        public string GetDescription()
            => !string.IsNullOrWhiteSpace(StateAttribute.Desc)
                ? StateAttribute.Desc
                : PropertyInfo.Name;
    }
}
