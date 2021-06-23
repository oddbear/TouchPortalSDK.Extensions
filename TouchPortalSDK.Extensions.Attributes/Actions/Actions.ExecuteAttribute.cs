// ReSharper disable CheckNamespace

namespace TouchPortalSDK.Extensions.Attributes
{
    public static partial class Actions
    {
        public class ExecuteAttribute : ActionAttribute
        {
            public override string Type => "execute";

            public string ExecutionType { get; set; } = null;
            public string ExecutionCmd { get; set; } = null;
        }
    }
}
