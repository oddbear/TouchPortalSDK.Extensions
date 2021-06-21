using System;
using System.Globalization;

namespace TouchPortalSDK.Extensions.Attributes
{
    public static class Setting
    {
        public abstract class SettingAttribute : Attribute
        {
            public string Name { get; }
            public string Type { get; }
            public string Default { get; }
            public bool IsPassword { get; }
            public bool ReadOnly { get; }

            protected SettingAttribute(string name, string type, string @default, bool isPassword, bool readOnly)
            {
                Name = name;
                Type = type;
                Default = @default;
                IsPassword = isPassword;
                ReadOnly = readOnly;
            }
        }

        [AttributeUsage(AttributeTargets.Property)]
        public class TextAttribute : SettingAttribute
        {
            public int? MaxLength { get; }

            public TextAttribute(string name = null,
                                 string @default = null,
                                 string type = null,
                                 int maxLength = int.MinValue,
                                 bool isPassword = false,
                                 bool readOnly = false)
                : base(name, type, @default, isPassword, readOnly)
            {
                if (maxLength > 0)
                    MaxLength = maxLength;
            }
        }

        [AttributeUsage(AttributeTargets.Property)]
        public class NumberAttribute : SettingAttribute
        {
            public double? MinValue { get; }
            public double? MaxValue { get; }

            public NumberAttribute(string name = null,
                                   double @default = double.NaN,
                                   string type = null,
                                   bool isPassword = false,
                                   double minValue = double.NaN,
                                   double maxValue = double.NaN,
                                   bool readOnly = false)
                : base(name, type, DoubleToString(@default), isPassword, readOnly)
            {
                if (!double.IsNaN(minValue))
                    MinValue = minValue;

                if (!double.IsNaN(maxValue))
                    MaxValue = maxValue;
            }

            private static string DoubleToString(double value)
            {
                return double.IsNaN(value)
                    ? null
                    : value.ToString(CultureInfo.InvariantCulture);
            }
        }
    }
}
