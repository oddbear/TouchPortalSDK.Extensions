using System.Collections.Generic;
using NUnit.Framework;
using TouchPortalSDK.Extensions.Attributes.Attributes;
using TouchPortalSDK.Extensions.Attributes.Reflection;
using TouchPortalSDK.Extensions.Attributes.Reflection.Contexts;

namespace TouchPortalSDK.Extensions.Attributes.Tests.EntryFile.Settings
{
    [Plugin]
    public class Settings_Some_Test
    {
        [Setting(name:"name", @default: "default", type:"type",maxLength:"maxLength",isPassword:true,minValue:"minValue",maxValue:"maxValue",readOnly:true)]
        public string TextSetting { get; set; }

        [Setting(name: "name", @default: "default", type: "type", maxLength: "maxLength", isPassword: true, minValue: "minValue", maxValue: "maxValue", readOnly: true)]
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
            Assert.AreEqual("name", _textSetting["name"]);
            Assert.AreEqual("name", _numberSetting["name"]);
        }
    }
}
