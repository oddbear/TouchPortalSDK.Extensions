using System;
using System.Linq;

namespace TouchPortalSDK.Extensions.Attributes
{
    public static class Data
    {
        [AttributeUsage(AttributeTargets.Parameter)]
        public abstract class DataAttribute : Attribute
        {
            public string Id { get; }
            public string Type { get; }
            public string Label { get; }
            public object Default { get; }

            protected DataAttribute(string id, string type, string label, object @default)
            {
                Id = id;
                Type = type;
                Label = label;
                Default = @default;
            }
        }

        [AttributeUsage(AttributeTargets.Parameter)]
        public class TextAttribute : DataAttribute
        {
            public TextAttribute(string id = null,
                                 string label = null,
                                 string @default = null)
                : base(id, "text", label, @default ?? string.Empty)
            {
                //
            }
        }

        [AttributeUsage(AttributeTargets.Parameter)]
        public class ChoiceAttribute : DataAttribute
        {
            public string[] ValueChoices { get; }

            public ChoiceAttribute(string id = null, string label = null, string @default = null, string[] valueChoices = null)
                : base(id, "choice", label, @default ?? string.Empty)
            {
                ValueChoices = valueChoices ?? Array.Empty<string>();
            }
        }

        [AttributeUsage(AttributeTargets.Parameter)]
        public class FolderAttribute : DataAttribute
        {
            public FolderAttribute(string id = null,
                                   string label = null,
                                   string @default = null)
                : base(id, "folder", label, @default ?? string.Empty)
            {
                //
            }
        }

        [AttributeUsage(AttributeTargets.Parameter)]
        public class FileAttribute : DataAttribute
        {
            public string[] Extensions { get; }

            public FileAttribute(string id = null,
                                 string label = null,
                                 string @default = null,
                                 string[] extensions = null)
                : base(id, "file", label, @default ?? string.Empty)
            {
                Extensions = extensions ?? Array.Empty<string>();
            }
        }

        [AttributeUsage(AttributeTargets.Parameter)]
        public class NumberAttribute : DataAttribute
        {
            public bool? AllowDecimals { get; }
            public double? MinValue { get; }
            public double? MaxValue { get; }

            public NumberAttribute(string id = null,
                                   string label = null,
                                   double @default = 0,
                                   bool allowDecimals = true,
                                   double minValue = double.NegativeInfinity,
                                   double maxValue = double.PositiveInfinity)
                : base(id, "number", label, @default)
            {
                AllowDecimals = allowDecimals;

                if (minValue > double.NegativeInfinity)
                    MinValue = minValue;

                if (maxValue < double.PositiveInfinity)
                    MaxValue = maxValue;
            }
        }

        [AttributeUsage(AttributeTargets.Parameter)]
        public class SwitchAttribute : DataAttribute
        {
            public SwitchAttribute(string id = null,
                                   string label = null,
                                   bool @default = false)
                : base(id, "switch", label, @default)
            {
                //
            }
        }

        [AttributeUsage(AttributeTargets.Parameter)]
        public class ColorAttribute : DataAttribute
        {
            public ColorAttribute(string id = null,
                                  string label = null,
                                  string @default = null)
                : base(id, "color", label, @default ?? "#00000000")
            {
                //
            }
        }
    }
}
