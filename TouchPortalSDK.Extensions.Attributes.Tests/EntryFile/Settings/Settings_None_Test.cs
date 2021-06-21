using System.Collections.Generic;
using NUnit.Framework;
using TouchPortalSDK.Extensions.Attributes;
using TouchPortalSDK.Extensions.Reflection.Contexts;

namespace TouchPortalSDK.Extensions.Reflection.Tests.EntryFile.Settings
{
    [Plugin]
    public class Settings_None_Test
    {
        [Setting.Text]
        public string TextSetting { get; set; }

        [Setting.Number]
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
            Assert.AreEqual("TextSetting", _textSetting["name"]);
            Assert.AreEqual("NumberSetting", _numberSetting["name"]);
        }

        [Test]
        public void Setting_Type_Empty_Test()
        {
            Assert.AreEqual("text", _textSetting["type"]);
            Assert.AreEqual("number", _numberSetting["type"]);
        }

        //[Test]
        //public void Setting_Default_Empty_Test()
        //{
        //    Assert.AreEqual("default", _category["default"]);
        //}

        [Test]
        public void Setting_ReadOnly_Empty_Test()
        {
            Assert.AreEqual(false, _textSetting["readOnly"]);
            Assert.AreEqual(false, _numberSetting["readOnly"]);
        }

        //[Test]
        //public void Setting_MaxLength_Empty_Test()
        //{
        //    Assert.AreEqual("maxLength", _category["maxLength"]);
        //}

        [Test]
        public void Setting_IsPassword_Empty_Test()
        {
            Assert.AreEqual(false, _textSetting["isPassword"]);
            Assert.AreEqual(false, _numberSetting["isPassword"]);
        }

        //[Test]
        //public void Setting_MinValue_Empty_Test()
        //{
        //    Assert.AreEqual("minValue", _category["minValue"]);
        //}

        //[Test]
        //public void Setting_MaxValue_Empty_Test()
        //{
        //    Assert.AreEqual("maxValue", _category["maxValue"]);
        //}
    }
}
