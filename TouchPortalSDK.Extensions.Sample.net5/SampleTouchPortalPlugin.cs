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
        [Event]
        [State(category: nameof(Categories.Category1))]
        public string Value { get; set; }

        [Action(category: nameof(Categories.Category1), name: "DoWork2")]
        public Task DoWork1()
        {
            return Task.CompletedTask;
        }

        [Action(category: nameof(Categories.Category2), name:"DoWork2")]
        public void DoWork2()
        {
            //
        }
    }
}
