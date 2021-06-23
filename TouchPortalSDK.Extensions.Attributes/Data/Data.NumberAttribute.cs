using System;

// ReSharper disable CheckNamespace

namespace TouchPortalSDK.Extensions.Attributes
{
    public static partial class Data
    {
        [AttributeUsage(AttributeTargets.Parameter)]
        public class NumberAttribute : DataAttribute
        {
            public override string Type => "number";

            public double Default { get; set; } = 0;

            public bool AllowDecimals { get; set; } = true;
            public double MinValue { get; set; } = double.NaN;
            public double MaxValue { get; set; } = double.NaN;
        }
    }
}
