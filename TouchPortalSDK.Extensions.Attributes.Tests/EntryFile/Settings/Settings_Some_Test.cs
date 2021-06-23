using System.Collections.Generic;
using NUnit.Framework;
using TouchPortalSDK.Extensions.Attributes;
using TouchPortalSDK.Extensions.Reflection.Contexts;

namespace TouchPortalSDK.Extensions.Reflection.Tests.EntryFile.Settings
{
    [Plugin]
    public class Settings_Some_Test
    {
        [Attributes.Settings.Text(Name = "name", Default = "default", MaxLength = 5, IsPassword = true, ReadOnly = true)]
        public string TextSetting { get; set; }

        [Attributes.Settings.Number(Name = "name", Default = 3, IsPassword = true, MinValue = 1, MaxValue = 5, ReadOnly = true)]
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
        public void Setting_Default_Empty_Test()
        {
            Assert.AreEqual("default", _textSetting["default"]);
            Assert.AreEqual("3", _numberSetting["default"]);
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

        [Test]
        public void Setting_MaxLength_Empty_Test()
        {
            Assert.AreEqual(5, _textSetting["maxLenght"]);
            Assert.False(_numberSetting.ContainsKey("maxLenght"));
            Assert.False(_numberSetting.ContainsKey("maxLength"));
        }
        
        [Test]
        public void Setting_MinValue_Empty_Test()
        {
            Assert.False(_textSetting.ContainsKey("minValue"));
            Assert.AreEqual(1, _numberSetting["minValue"]);
        }

        [Test]
        public void Setting_MaxValue_Empty_Test()
        {
            Assert.False(_textSetting.ContainsKey("maxValue"));
            Assert.AreEqual(5, _numberSetting["maxValue"]);
        }
    }
}
