using System.Collections.Generic;
using NUnit.Framework;
using TouchPortalSDK.Extensions.Attributes.Attributes;
using TouchPortalSDK.Extensions.Attributes.Reflection;
using TouchPortalSDK.Extensions.Attributes.Reflection.Contexts;
using CategoryAttribute = TouchPortalSDK.Extensions.Attributes.Attributes.CategoryAttribute;

namespace TouchPortalSDK.Extensions.Attributes.Tests.EntryFile.Categories
{
    [Plugin]
    public class Categories_Enum_Some_Test
    {
        public enum Categories
        {
            [Category(id: "categoryId", name: "Category Name", imagePath: "imagePath")]
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
        public void Category_Id_Set_Test()
        {
            Assert.AreEqual("categoryId", _category["id"]);
        }

        [Test]
        public void Category_Name_Set_Test()
        {
            Assert.AreEqual("Category Name", _category["name"]);
        }

        [Test]
        public void Category_ImagePath_Set_Test()
        {
            Assert.AreEqual("imagePath", _category["imagepath"]);
        }
    }
}