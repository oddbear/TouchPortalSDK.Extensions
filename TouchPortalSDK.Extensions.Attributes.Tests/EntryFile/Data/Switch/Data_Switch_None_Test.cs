using System.Collections.Generic;
using NUnit.Framework;
using TouchPortalSDK.Extensions.Attributes;
using TouchPortalSDK.Extensions.Reflection.Contexts;

namespace TouchPortalSDK.Extensions.Reflection.Tests.EntryFile.Data.Switch
{
    [Plugin]
    public class Data_Switch_None_Test
    {
        private PluginContext _pluginContext;
        private string _pluginId;
        private Dictionary<string, object> _category;
        private Dictionary<string, object> _action;
        private Dictionary<string, object> _data;

        [Attributes.Actions.Communicate]
        public void Action([Attributes.Data.Switch] string value)
        {
            //
        }

        [SetUp]
        public void Setup()
        {
            _pluginContext = PluginTreeBuilder.Build(this.GetType());
            _pluginId = _pluginContext.GetId();

            var entryFile = EntryFileBuilder.BuildEntryFile(_pluginContext);
            var categories = (List<object>)entryFile["categories"];
            _category = (Dictionary<string, object>)categories[0];
            var actions = (List<object>)_category["actions"];
            _action = (Dictionary<string, object>) actions[0];
            var datas = (List<object>)_action["data"];
            _data = (Dictionary<string, object>)datas[0];
        }

        [Test]
        public void Action_Format_Set_Test()
        {
            var dataId = _pluginId + ".DefaultCategory.action." + nameof(Action) + ".data.value";
            Assert.AreEqual($"{{${dataId}$}}", _action["format"]);
        }

        [Test]
        public void Data_Id_Empty_Test()
        {
            Assert.AreEqual(_pluginId + ".DefaultCategory.action." + nameof(Action) + ".data.value", _data["id"]);
        }

        [Test]
        public void Data_Type_Empty_Test()
        {
            Assert.AreEqual("switch", _data["type"]);
        }

        [Test]
        public void Data_Label_Empty_Test()
        {
            Assert.AreEqual("value", _data["label"]);
        }

        [Test]
        public void Data_Default_Empty_Test()
        {
            Assert.AreEqual(false, _data["default"]);
        }

        [Test]
        public void Data_ValueChoices_Empty_Test()
        {
            Assert.False(_data.ContainsKey("valueChoices"));
        }

        [Test]
        public void Data_Extensions_Empty_Test()
        {
            Assert.False(_data.ContainsKey("extensions"));
        }

        [Test]
        public void Data_AllowDecimals_Empty_Test()
        {
            Assert.False(_data.ContainsKey("allowDecimals"));
        }

        [Test]
        public void Data_MinValue_Empty_Test()
        {
            Assert.False(_data.ContainsKey("minValue"));
        }

        [Test]
        public void Data_MaxValue_Empty_Test()
        {
            Assert.False(_data.ContainsKey("maxValue"));
        }
    }
}