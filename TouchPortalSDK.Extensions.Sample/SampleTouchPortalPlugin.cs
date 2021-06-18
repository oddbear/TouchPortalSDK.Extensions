using System;
using System.Threading.Tasks;
using TouchPortalSDK.Extensions.Attributes.Attributes;

namespace TouchPortalSDK.Extensions.Sample
{
    [Plugin]
    public class SampleTouchPortalPlugin : TouchPortalPlugin
    {
        //TODO: Is there a more .Net friendly way of setting this?
        public enum Categories
        {
            [Category(name: "Category1")]
            Category1,

            [Category(name: "Category2")]
            Category2
        }

        public SampleTouchPortalPlugin()
        {
            //RegisterState(StateValue)
        }

        //TODO: How to signal a change? PropertyChanged? Setting<string>.Value <-- ?
        [Setting.Text]
        public string TextSetting { get; } //Settings does not have any setters, unless they are ReadOnly.

        //TODO: How to signal a change? PropertyChanged? Setting<int>.Value <-- ?
        [Setting.Number]
        public int NumberSetting { get; } //Settings does not have any setters, unless they are ReadOnly.

        //TODO: Subtypes for ex. extensions etc.
        [Event]
        [State(category: nameof(Categories.Category1))]
        public string StateValue { get; set; }

        //TODO: How to generate format in a good way?
        [Action(category: nameof(Categories.Category1))]
        public Task SomethingAsync()
        {
            //TODO: Might want a way to generate a Const file etc., so we can use IDs other place. Ex. GoXLR app for mappings?
            //TODO: Why cant we do this in runtime? Ex. GetId<SampleTouchPortalPlugin>.Action(p => p.DoWork1).Id? If from base... Generic might not be needed.
            return Task.CompletedTask;
        }

        [Action(category: nameof(Categories.Category2), format: "Text: {value}")]
        public void SelectText([Data.Text] string value)
        {
            //Something like this?
            //base.UpdateState(StateValue, DateTime.Now); //Can also use the propertyChanged pattern.
        }

        [Action(category: nameof(Categories.Category1), format: "Number: {0}")]
        public void SelectNumber([Data.Number] int value)
        {
            //
        }

        [Action(category: nameof(Categories.Category2))]
        public void SelectFile([Data.File(extensions: new[] { "*.png" })] string filePath)
        {
            //
        }

        [Action(category: nameof(Categories.Category2))]
        public void SelectFolder([Data.Folder] string folderPath)
        {
            //
        }
    }
}
