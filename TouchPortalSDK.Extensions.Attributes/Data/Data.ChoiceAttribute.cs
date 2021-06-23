using System;

// ReSharper disable CheckNamespace

namespace TouchPortalSDK.Extensions.Attributes
{
    public static partial class Data
    {
        [AttributeUsage(AttributeTargets.Parameter)]
        public class ChoiceAttribute : DataAttribute
        {
            public override string Type => "choice";

            public string Default { get; set; } = string.Empty;

            public string[] ValueChoices { get; set; } = Array.Empty<string>();
        }
    }
}
