using System.Collections.Generic;
using NUnit.Framework;
using TouchPortalSDK.Extensions.Attributes;
using TouchPortalSDK.Extensions.Reflection.Contexts;
using CategoryAttribute = TouchPortalSDK.Extensions.Attributes.CategoryAttribute;

namespace TouchPortalSDK.Extensions.Reflection.Tests.EntryFile.Categories
{
    [Plugin]
    public class Categories_Enum_None_Test
    {
        public enum Categories
        {
            [Category]
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