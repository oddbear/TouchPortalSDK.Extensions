using System;

// ReSharper disable CheckNamespace

namespace TouchPortalSDK.Extensions.Attributes
{
    public static partial class Settings
    {
        [AttributeUsage(AttributeTargets.Property)]
        public class TextAttribute : SettingAttribute
        {
            public override string Type => "text";

            public string Default { get; set; }

            public int MaxLength { get; set; } = int.MinValue;
        }
    }
}
