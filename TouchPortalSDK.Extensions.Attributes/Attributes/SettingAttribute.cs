using System;

namespace TouchPortalSDK.Extensions.Attributes.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class SettingAttribute : Attribute
    {
        public string Name { get; }
        public string Default { get; }
        public string Type { get; }
        public string MaxLength { get; }
        public bool IsPassword { get; }
        public string MinValue { get; }
        public string MaxValue { get; }
        public bool ReadOnly { get; }

        public SettingAttribute(string name = null,
                                 string @default = null,
                                 string type = null,
                                 string maxLength = null,
                                 bool isPassword = false,
                                 string minValue = null,
                                 string maxValue = null,
                                 bool readOnly = false)
        {
            Name = name;
            Default = @default;
            Type = type;
            MaxLength = maxLength;
            IsPassword = isPassword;
            MinValue = minValue;
            MaxValue = maxValue;
            ReadOnly = readOnly;
        }
    }
}
