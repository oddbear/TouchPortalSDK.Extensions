using System;

// ReSharper disable CheckNamespace

namespace TouchPortalSDK.Extensions.Attributes
{
    public static partial class Settings
    {
        public abstract class SettingAttribute : Attribute
        {
            public string Name { get; set; }
            public abstract string Type { get; }
            public bool IsPassword { get; set; } = false;
            public bool ReadOnly { get; set; } = false;
        }
    }
}
