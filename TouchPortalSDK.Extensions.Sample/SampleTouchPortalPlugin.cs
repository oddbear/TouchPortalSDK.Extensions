using System.Threading.Tasks;
using TouchPortalSDK.Extensions.Attributes;

namespace TouchPortalSDK.Extensions.Sample
{
    [Plugin]
    public class SampleTouchPortalPlugin : TouchPortalPlugin
    {
        public enum Categories
        {
            [Category(Name = "TP Ext Sample Cat 1")]
            Category1,

            [Category(Name = "TP Ext Sample Cat 2")]
            Category2
        }

        public SampleTouchPortalPlugin()
        {
            //RegisterState(StateValue)
        }

        //TODO: How to signal a change? PropertyChanged? Setting<string>.Value <-- ?
        [Settings.Text]
        public string TextSetting { get; } //Settings does not have any setters, unless they are ReadOnly.

        //TODO: How to signal a change? PropertyChanged? Setting<int>.Value <-- ?
        [Settings.Number]
        public int NumberSetting { get; } //Settings does not have any setters, unless they are ReadOnly.

        //TODO: Subtypes for ex. extensions etc.
        [Events.Communicate]
        [States.Choice(Category = nameof(Categories.Category1))]
        public string StateValue { get; set; }

        //TODO: How to generate format in a good way?
        [Actions.Communicate(Category = nameof(Categories.Category1))]
        public Task SomethingAsync()
        {
            //TODO: Might want a way to generate a Const file etc., so we can use IDs other place. Ex. GoXLR app for mappings?
            //TODO: Why cant we do this in runtime? Ex. GetId<SampleTouchPortalPlugin>.Action(p => p.DoWork1).Id? If from base... Generic might not be needed.
            return Task.CompletedTask;
        }

        [Actions.Communicate(Category = nameof(Categories.Category2), Format = "Text: {value}")]
        public void SelectText([Data.Text] string value)
        {
            //Something like this?
            //base.UpdateState(StateValue, DateTime.Now); //Can also use the propertyChanged pattern.
        }

        [Actions.Communicate(Category = nameof(Categories.Category1), Format = "Number: {0}")]
        public void SelectNumber([Data.Number] int value)
        {
            //
        }

        [Actions.Communicate(Category = nameof(Categories.Category2))]
        public void SelectFile([Data.File(Extensions = new[] { "*.png" })] string filePath)
        {
            //
        }

        [Actions.Communicate(Category = nameof(Categories.Category2))]
        public void SelectFolder([Data.Folder] string folderPath)
        {
            //
        }
    }
}
