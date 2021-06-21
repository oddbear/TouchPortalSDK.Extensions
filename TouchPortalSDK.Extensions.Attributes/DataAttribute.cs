using System;

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

            protected DataAttribute(string id, string type, string label)
            {
                Id = id;
                Type = type;
                Label = label;
            }
        }

        [AttributeUsage(AttributeTargets.Parameter)]
        public class TextAttribute : DataAttribute
        {
            public TextAttribute(string id = null,
                                 string label = null)
                : base(id, "text", label)
            {
                //
            }
        }

        [AttributeUsage(AttributeTargets.Parameter)]
        public class ChoiceAttribute : DataAttribute
        {
            public string[] ValueChoices { get; set; }

            public ChoiceAttribute(string id = null, string label = null, string[] valueChoices = null)
                : base(id, "choice", label)
            {
                ValueChoices = valueChoices ?? Array.Empty<string>();
            }
        }

        [AttributeUsage(AttributeTargets.Parameter)]
        public class FolderAttribute : DataAttribute
        {
            public FolderAttribute(string id = null,
                                   string label = null)
                : base(id, "folder", label)
            {
                //
            }
        }

        [AttributeUsage(AttributeTargets.Parameter)]
        public class FileAttribute : DataAttribute
        {
            public string[] Extensions { get; set; }

            public FileAttribute(string id = null,
                                 string label = null,
                                 string[] extensions = null)
                : base(id, "file", label)
            {
                Extensions = extensions;
            }
        }

        [AttributeUsage(AttributeTargets.Parameter)]
        public class NumberAttribute : DataAttribute
        {
            public bool AllowDecimals { get; protected set; }
            public object MinValue { get; protected set; }
            public object MaxValue { get; protected set; }

            public NumberAttribute(string id = null,
                                   string label = null,
                                   bool allowDecimals = true,
                                   object minValue = null,
                                   object maxValue = null)
                : base(id, "number", label)
            {
                //TODO: Verifications of min and max.
                AllowDecimals = allowDecimals;
                MinValue = minValue;
                MaxValue = maxValue;
            }
        }

        [AttributeUsage(AttributeTargets.Parameter)]
        public class SwitchAttribute : DataAttribute
        {
            public SwitchAttribute(string id = null,
                string label = null)
                : base(id, "switch", label)
            {
                //
            }
        }

        [AttributeUsage(AttributeTargets.Parameter)]
        public class ColorAttribute : DataAttribute
        {
            public ColorAttribute(string id = null,
                string label = null)
                : base(id, "color", label)
            {
                //
            }
        }
    }
}
