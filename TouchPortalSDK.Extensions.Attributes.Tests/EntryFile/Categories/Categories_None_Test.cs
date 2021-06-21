using System.Collections.Generic;
using NUnit.Framework;
using TouchPortalSDK.Extensions.Attributes;
using TouchPortalSDK.Extensions.Reflection.Contexts;

namespace TouchPortalSDK.Extensions.Reflection.Tests.EntryFile.Categories
{
    [Plugin(name: "test plugin")]
    public class Categories_None_Test
    {
        private PluginContext _pluginContext;
        private string _pluginId;
        private Dictionary<string, object> _category;

        [SetUp]
        public void Setup()
        {
            _pluginContext = PluginTreeBuilder.Build(this.GetType());
            _pluginId = _pluginContext.GetId();

            var entryFile = EntryFileBuilder.BuildEntryFile(_pluginContext);
            var categories = (List<object>)entryFile["categories"];
            _category = (Dictionary<string, object>)categories[0];
        }

        [Test]
        public void Category_Id_Empty_Test()
        {
            Assert.AreEqual(_pluginId + ".DefaultCategory", _category["id"]);
        }

        [Test]
        public void Category_Name_Empty_Test()
        {
            Assert.AreEqual("test plugin", _category["name"]);
        }

        [Test]
        public void Category_ImagePath_Set_Test()
        {
            Assert.Throws<KeyNotFoundException>(() => _ =_category["imagepath"]);
        }
    }
}