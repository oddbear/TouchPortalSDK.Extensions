using System.Reflection;
using TouchPortalSDK.Extensions.Attributes;

namespace TouchPortalSDK.Extensions.Reflection.Contexts
{
    public class ActionContext
    {
        public CategoryContext CategoryContext { get; }

        public ActionAttribute ActionAttribute { get; }
        public MethodInfo MethodInfo { get; }

        public ActionContext(CategoryContext categoryContext, ActionAttribute actionAttribute, MethodInfo methodInfo)
        {
            CategoryContext = categoryContext;
            ActionAttribute = actionAttribute;
            MethodInfo = methodInfo;
        }

        public string GetId()
            => !string.IsNullOrWhiteSpace(ActionAttribute.Id)
                ? ActionAttribute.Id
                : $"{CategoryContext.GetId()}.action.{MethodInfo.Name}";

        public string GetName()
            => !string.IsNullOrWhiteSpace(ActionAttribute.Name)
                ? ActionAttribute.Name
                : MethodInfo.Name;
    }
}
