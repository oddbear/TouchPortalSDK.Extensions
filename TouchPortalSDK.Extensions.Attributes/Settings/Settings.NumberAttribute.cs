using System;

// ReSharper disable CheckNamespace

namespace TouchPortalSDK.Extensions.Attributes
{
    public static partial class Settings
    {
        [AttributeUsage(AttributeTargets.Property)]
        public class NumberAttribute : SettingAttribute
        {
            public override string Type => "number";

            public double Default { get; set; } = double.NaN;

            public double MinValue { get; set; } = double.NaN;
            public double MaxValue { get; set; } = double.NaN;
        }
    }
}
