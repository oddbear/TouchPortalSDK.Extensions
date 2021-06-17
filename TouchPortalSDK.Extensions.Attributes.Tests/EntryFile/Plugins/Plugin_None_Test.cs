using NUnit.Framework;
using TouchPortalSDK.Extensions.Attributes.Attributes;
using TouchPortalSDK.Extensions.Attributes.Reflection;
using TouchPortalSDK.Extensions.Attributes.Reflection.Contexts;

namespace TouchPortalSDK.Extensions.Attributes.Tests.EntryFile.Plugins
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
    }
}