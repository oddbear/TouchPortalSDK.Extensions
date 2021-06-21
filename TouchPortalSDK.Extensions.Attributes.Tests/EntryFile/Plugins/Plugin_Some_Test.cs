using NUnit.Framework;
using TouchPortalSDK.Extensions.Attributes;
using TouchPortalSDK.Extensions.Reflection.Contexts;

namespace TouchPortalSDK.Extensions.Reflection.Tests.EntryFile.Plugins
{
    [Plugin(id:"pluginId", name:"Plugin Name")]
    public class Plugin_Some_Test
    {
        private PluginContext _pluginContext;

        [SetUp]
        public void Setup()
        {
            _pluginContext = PluginTreeBuilder.Build(this.GetType());
        }

        [Test]
        public void Plugin_Id_Set_Test()
        {
            var entryFile = EntryFileBuilder.BuildEntryFile(_pluginContext);

            Assert.AreEqual("pluginId", entryFile["id"]);
        }

        [Test]
        public void Plugin_Name_Set_Test()
        {
            var entryFile = EntryFileBuilder.BuildEntryFile(_pluginContext);

            Assert.AreEqual("Plugin Name", entryFile["name"]);
        }
    }
}
