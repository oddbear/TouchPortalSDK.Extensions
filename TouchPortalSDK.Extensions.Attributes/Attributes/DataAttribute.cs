using System;

namespace TouchPortalSDK.Extensions.Attributes.Attributes
{
    public static class Data
    {
        [AttributeUsage(AttributeTargets.Parameter)]
        public abstract class DataAttribute : Attribute
        {
            public string Id { get; }
            public string Label { get; }

            protected DataAttribute(string id, string label)
            {
                Id = id;
                Label = label;
            }
        }

        [AttributeUsage(AttributeTargets.Parameter)]
        public class TextAttribute : DataAttribute
        {
            public TextAttribute(string id = null,
                                 string label = null)
                : base(id, label)
            {
                //
            }
        }

        [AttributeUsage(AttributeTargets.Parameter)]
        public class ChoiceAttribute : DataAttribute
        {
            public ChoiceAttribute(string id = null, string label = null)
                : base(id, label)
            {
                //
            }
        }

        [AttributeUsage(AttributeTargets.Parameter)]
        public class FolderAttribute : DataAttribute
        {
            public FolderAttribute(string id = null,
                                   string label = null)
                : base(id, label)
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
                : base(id, label)
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
                : base(id, label)
            {
                //TODO: Verifications of min and max.
                AllowDecimals = allowDecimals;
                MinValue = minValue;
                MaxValue = maxValue;
            }
        }
    }
}
