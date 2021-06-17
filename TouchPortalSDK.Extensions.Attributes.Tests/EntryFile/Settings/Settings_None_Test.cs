using System.Collections.Generic;
using NUnit.Framework;
using TouchPortalSDK.Extensions.Attributes.Attributes;
using TouchPortalSDK.Extensions.Attributes.Reflection;
using TouchPortalSDK.Extensions.Attributes.Reflection.Contexts;

namespace TouchPortalSDK.Extensions.Attributes.Tests.EntryFile.Settings
{
    [Plugin]
    public class Settings_None_Test
    {
        [Setting]
        public string TextSetting { get; set; }

        [Setting]
        public int NumberSetting { get; set; }

        public enum Categories
        {
            [Attributes.Category(id: "categoryId", name: "Category Name", imagePath: "imagePath")]
            Category1
        }

        private PluginContext _pluginContext;
        private Dictionary<string, object> _textSetting;
        private Dictionary<string, object> _numberSetting;

        [SetUp]
        public void Setup()
        {
            _pluginContext = PluginTreeBuilder.Build(this.GetType());

            var entryFile = EntryFileBuilder.BuildEntryFile(_pluginContext);
            var settings = (List<object>)entryFile["settings"];
            _textSetting = (Dictionary<string, object>)settings[0];
            _numberSetting = (Dictionary<string, object>)settings[1];
        }

        [Test]
        public void Setting_Name_Empty_Test()
        {
            Assert.AreEqual("TextSetting", _textSetting["name"]);
            Assert.AreEqual("NumberSetting", _numberSetting["name"]);
        }

        //[Test]
        //public void Setting_Type_Empty_Test()
        //{
        //    Assert.AreEqual("categoryId", _category["id"]);
        //}

        //[Test]
        //public void Setting_Default_Empty_Test()
        //{
        //    Assert.AreEqual("imagePath", _category["imagepath"]);
        //}

        //[Test]
        //public void Setting_ReadOnly_Empty_Test()
        //{
        //    Assert.AreEqual("imagePath", _category["imagepath"]);
        //}

        //[Test]
        //public void Setting_MaxLength_Empty_Test()
        //{
        //    Assert.AreEqual("imagePath", _category["imagepath"]);
        //}

        //[Test]
        //public void Setting_MinValue_Empty_Test()
        //{
        //    Assert.AreEqual("imagePath", _category["imagepath"]);
        //}

        //[Test]
        //public void Setting_MaxValue_Empty_Test()
        //{
        //    Assert.AreEqual("imagePath", _category["imagepath"]);
        //}
    }
}
