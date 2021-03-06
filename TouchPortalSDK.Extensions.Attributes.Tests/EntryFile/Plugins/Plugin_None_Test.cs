using System.Collections.Generic;
using NUnit.Framework;
using TouchPortalSDK.Extensions.Attributes;
using TouchPortalSDK.Extensions.Reflection.Contexts;

namespace TouchPortalSDK.Extensions.Reflection.Tests.EntryFile.Plugins
{
    public class Plugin_None_Test
    {
        [Plugin] //Some tests hate generics in the test runner. Need to separate this from the test class.
        public class PluginClass<TGenericsTest>
        {
            //
        }

        private PluginContext _pluginContext;
        private string _namespace;
        private string _name;

        [SetUp]
        public void Setup()
        {
            var type = new PluginClass<object>().GetType();
            _namespace = type.Namespace;
            _name = "PluginClass";
            _pluginContext = PluginTreeBuilder.Build(type);
        }

        [Test]
        public void Plugin_Id_Empty_Test()
        {
            var entryFile = EntryFileBuilder.BuildEntryFile(_pluginContext);
            
            Assert.AreEqual(_namespace + "." + _name, entryFile["id"]);
        }

        [Test]
        public void Plugin_Name_Empty_Test()
        {
            var entryFile = EntryFileBuilder.BuildEntryFile(_pluginContext);

            Assert.AreEqual(_name, entryFile["name"]);
        }

        [Test]
        public void Plugin_Sdk_Empty_Test()
        {
            var entryFile = EntryFileBuilder.BuildEntryFile(_pluginContext);

            Assert.AreEqual(3, entryFile["sdk"]);
        }

        [Test]
        public void Plugin_Version_Empty_Test()
        {
            var entryFile = EntryFileBuilder.BuildEntryFile(_pluginContext);

            Assert.AreEqual(1, entryFile["version"]);
        }

        [Test]
        public void Plugin_Configuration_Empty_Test()
        {
            var entryFile = EntryFileBuilder.BuildEntryFile(_pluginContext);
            
            Assert.False(entryFile.ContainsKey("configuration"));
        }
    }
}