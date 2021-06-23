using System.Collections.Generic;
using NUnit.Framework;
using TouchPortalSDK.Extensions.Attributes;
using TouchPortalSDK.Extensions.Reflection.Contexts;

namespace TouchPortalSDK.Extensions.Reflection.Tests.EntryFile.Plugins
{
    [Plugin(Id = "pluginId", Name = "Plugin Name", Sdk = 4, Version = 2, ColorDark = "#000000", ColorLight = "#FFFFFF")]
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

        [Test]
        public void Plugin_Sdk_Set_Test()
        {
            var entryFile = EntryFileBuilder.BuildEntryFile(_pluginContext);

            Assert.AreEqual(4, entryFile["sdk"]);
        }

        [Test]
        public void Plugin_Version_Set_Test()
        {
            var entryFile = EntryFileBuilder.BuildEntryFile(_pluginContext);

            Assert.AreEqual(2, entryFile["version"]);
        }

        [Test]
        public void Plugin_ColorDark_Set_Test()
        {
            var entryFile = EntryFileBuilder.BuildEntryFile(_pluginContext);

            var configuration = (Dictionary<string, object>)entryFile["configuration"];

            Assert.AreEqual("#000000", configuration["colorDark"]);
        }

        [Test]
        public void Plugin_ColorLight_Set_Test()
        {
            var entryFile = EntryFileBuilder.BuildEntryFile(_pluginContext);

            var configuration = (Dictionary<string, object>)entryFile["configuration"];

            Assert.AreEqual("#FFFFFF", configuration["colorLight"]);
        }
    }
}
