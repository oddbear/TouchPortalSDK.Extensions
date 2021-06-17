using System.Collections.Generic;
using NUnit.Framework;
using TouchPortalSDK.Extensions.Attributes.Attributes;
using TouchPortalSDK.Extensions.Attributes.Reflection;
using TouchPortalSDK.Extensions.Attributes.Reflection.Contexts;

namespace TouchPortalSDK.Extensions.Attributes.Tests.EntryFile.Categories
{
    [Plugin]
    public class Categories_None_Test
    {
        public enum Categories
        {
            [Attributes.Category]
            Category1
        }

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
            Assert.AreEqual(_pluginId + ".Category1", _category["id"]);
        }

        [Test]
        public void Category_Name_Empty_Test()
        {
            Assert.AreEqual("Category1", _category["name"]);
        }

        [Test]
        public void Category_ImagePath_Set_Test()
        {
            Assert.Throws<KeyNotFoundException>(() => _ =_category["imagepath"]);
        }
    }
}