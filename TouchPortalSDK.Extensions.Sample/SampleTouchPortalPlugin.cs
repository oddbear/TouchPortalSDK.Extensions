using System.Threading.Tasks;
using TouchPortalSDK.Extensions.Attributes.Attributes;

namespace TouchPortalSDK.Extensions.Sample
{
    public enum Categories
    {
        [Category(name: "Category1")]
        Category1,

        [Category(name:"Category2")]
        Category2
    }

    [Plugin]
    public class SampleTouchPortalPlugin : TouchPortalPlugin
    {
        [Setting]
        public string TextSetting { get; set; }

        [Setting]
        public int NumberSetting { get; set; }

        //TODO: Subtypes for ex. extensions etc.
        [Event]
        [State(category: nameof(Categories.Category1))]
        public string Value { get; set; }

        //TODO: How to generate format in a good way?
        [Action(category: nameof(Categories.Category1), name: "DoWork2", format:"Format '{0}'")]
        public Task DoWork1([Data] string value1)
        {
            //TODO: Might want a way to generate a Const file etc., so we can use IDs other place. Ex. GoXLR app for mappings?
            //TODO: Why cant we do this in runtime? Ex. GetId<SampleTouchPortalPlugin>.Action(p => p.DoWork1).Id? If from base... Generic might not be needed.
            return Task.CompletedTask;
        }

        [Action(category: nameof(Categories.Category2), name:"DoWork2")]
        public void DoWork2([Data] string value1)
        {
            //
        }

        [Action(category: nameof(Categories.Category1))]
        public void SelectNumber([DataNumber] int value)
        {
            //
        }

        [Action(category: nameof(Categories.Category2))]
        public void SelectFile([DataFile(new[] { "*.png" })] string file)
        {
            //
        }
    }
}
