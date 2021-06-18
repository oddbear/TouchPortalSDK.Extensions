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
        [Setting.Text(name:"name", @default: "default", type:"text", maxLength:5, isPassword:true, readOnly:true)]
        public string TextSetting { get; set; }

        [Setting.Number(name: "name", @default: 3, type: "number", isPassword: true, minValue: 0, maxValue: 5, readOnly: true)]
        public int NumberSetting { get; set; }
        
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

        [Test]
        public void Setting_Type_Empty_Test()
        {
            Assert.AreEqual("text", _textSetting["type"]);
            Assert.AreEqual("number", _numberSetting["type"]);
        }

        [Test]
        public void Setting_ReadOnly_Empty_Test()
        {
            Assert.AreEqual(true, _textSetting["readOnly"]);
            Assert.AreEqual(true, _numberSetting["readOnly"]);
        }

        [Test]
        public void Setting_IsPassword_Empty_Test()
        {
            Assert.AreEqual(true, _textSetting["isPassword"]);
            Assert.AreEqual(true, _numberSetting["isPassword"]);
        }
    }
}
