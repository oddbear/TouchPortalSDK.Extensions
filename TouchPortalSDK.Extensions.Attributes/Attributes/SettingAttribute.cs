using System;

namespace TouchPortalSDK.Extensions.Attributes.Attributes
{
    public static class Setting
    {
        public abstract class SettingAttribute : Attribute
        {
            public string Name { get; }
            public string Type { get; }
            public string Default { get; protected set; }
            public int? MaxLength { get; protected set; }
            public bool IsPassword { get; }
            public object MinValue { get; protected set; }
            public object MaxValue { get; protected set; }
            public bool ReadOnly { get; }

            protected SettingAttribute(string name, string type, bool isPassword, bool readOnly)
            {
                Name = name;
                Type = type;
                IsPassword = isPassword;
                ReadOnly = readOnly;
            }
        }

        [AttributeUsage(AttributeTargets.Property)]
        public class TextAttribute : SettingAttribute
        {
            public TextAttribute(string name = null,
                                 string @default = null,
                                 string type = null,
                                 int maxLength = int.MinValue,
                                 bool isPassword = false,
                                 bool readOnly = false)
                : base(name, type, isPassword, readOnly)
            {
                Default = @default;

                if (maxLength > 0)
                    MaxLength = maxLength;
            }
        }

        [AttributeUsage(AttributeTargets.Property)]
        public class NumberAttribute : SettingAttribute
        {
            public NumberAttribute(string name = null,
                                   double @default = 0,
                                   string type = null,
                                   bool isPassword = false,
                                   double minValue = double.NegativeInfinity,
                                   double maxValue = double.PositiveInfinity,
                                   bool readOnly = false)
                : base(name, type, isPassword, readOnly)
            {
                if (@default > 0)
                    Default = @default.ToString(); //TODO: Find the correct culture settings for Touch Portal.

                if (minValue > double.NegativeInfinity)
                    MinValue = minValue;

                if (maxValue < double.PositiveInfinity)
                    MaxValue = maxValue;
            }
        }
    }
}
