using System;

// ReSharper disable CheckNamespace

namespace TouchPortalSDK.Extensions.Attributes
{
    public static partial class States
    {
        [AttributeUsage(AttributeTargets.Property)]
        public class TextAttribute : StateAttribute
        {
            public override string Type => "text";

            /// <summary>
            /// Color as "#FF115599", or Base64 representation of a square png image.
            /// </summary>
            public string Default { get; set; } = string.Empty;
        }
    }
}
