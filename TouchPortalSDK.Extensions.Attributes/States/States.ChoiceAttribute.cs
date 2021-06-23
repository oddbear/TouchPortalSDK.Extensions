using System;

// ReSharper disable CheckNamespace

namespace TouchPortalSDK.Extensions.Attributes
{
    public static partial class States
    {
        [AttributeUsage(AttributeTargets.Property)]
        public class ChoiceAttribute : StateAttribute
        {
            public override string Type => "choice";

            public string Default { get; set; } = string.Empty;

            public string[] ValueChoices { get; set; } = null;
        }
    }
}
