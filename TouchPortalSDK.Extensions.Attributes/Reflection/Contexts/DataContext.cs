using System.Reflection;
using TouchPortalSDK.Extensions.Attributes.Attributes;

namespace TouchPortalSDK.Extensions.Attributes.Reflection.Contexts
{
    public class DataContext
    {
        public ActionContext ActionContext { get; }
        public DataAttribute DataAttribute { get; }
        public MethodInfo MethodInfo { get; }
        public ParameterInfo ParameterInfo { get; }

        public DataContext(ActionContext actionContext, DataAttribute dataAttribute, MethodInfo methodInfo, ParameterInfo parameterInfo)
        {
            ActionContext = actionContext;
            DataAttribute = dataAttribute;
            MethodInfo = methodInfo;
            ParameterInfo = parameterInfo;
        }

        public string GetId()
            => !string.IsNullOrWhiteSpace(DataAttribute.Id)
                ? DataAttribute.Id
                : $"{ActionContext.GetId()}.{ParameterInfo.Name}";

        public string GetLabel()
            => !string.IsNullOrWhiteSpace(DataAttribute.Label)
                ? DataAttribute.Label
                : ParameterInfo.Name;
    }
}
