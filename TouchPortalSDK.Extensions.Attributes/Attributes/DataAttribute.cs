using System;

namespace TouchPortalSDK.Extensions.Attributes.Attributes
{
    [AttributeUsage(AttributeTargets.Parameter)]
    public class DataAttribute : Attribute
    {
        public string Id { get; }
        public string Label { get; }

        public DataAttribute(string id = null, string label = null)
        {
            Id = id;
            Label = label;
        }
    }

    [AttributeUsage(AttributeTargets.Parameter)]
    public class DataTextAttribute : DataAttribute
    {
        //
    }

    [AttributeUsage(AttributeTargets.Parameter)]
    public class DataChoiceAttribute : DataAttribute
    {
        //
    }

    [AttributeUsage(AttributeTargets.Parameter)]
    public class DataFolderAttribute : DataAttribute
    {
        public DataFolderAttribute()
            : base("todo", "todo")
        {
            //TODO: Verifications of min and max.
        }
    }

    [AttributeUsage(AttributeTargets.Parameter)]
    public class DataFileAttribute : DataAttribute
    {
        public DataFileAttribute(string[] extensions)
            : base("todo", "todo")
        {
            //TODO: Verifications of min and max.
        }
    }

    [AttributeUsage(AttributeTargets.Parameter)]
    public class DataNumberAttribute : DataAttribute
    {
        public DataNumberAttribute(bool allowDecimals = true, object minValue = null, object maxValue = null)
            : base("todo", "todo")
        {
            //TODO: Verifications of min and max.
        }
    }
}
