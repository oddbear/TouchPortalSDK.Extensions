using System.Collections.Generic;
using NUnit.Framework;
using TouchPortalSDK.Extensions.Reflection;
using TouchPortalSDK.Extensions.Reflection.Contexts;

namespace TouchPortalSDK.Extensions.Attributes.Tests.EntryFile.Data.Choice
{
    [Plugin]
    public class Data_Choice_Some_Test
    {
        private PluginContext _pluginContext;
        private string _pluginId;
        private Dictionary<string, object> _category;
        private Dictionary<string, object> _action;
        private Dictionary<string, object> _data;

        [Action]
        public void Action([Attributes.Data.Choice(id: "dataId", label: "Data Label")]string value)
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
        public void Data_Id_Empty_Test()
        {
            Assert.AreEqual("dataId", _data["id"]);
        }

        [Test]
        public void Data_Type_Empty_Test()
        {
            Assert.AreEqual("choice", _data["type"]);
        }

        [Test]
        public void Data_Label_Empty_Test()
        {
            Assert.AreEqual("Data Label", _data["label"]);
        }

        //default
        //valueChoices

        [Test]
        public void Data_Extensions_Empty_Test()
        {
            Assert.Throws<KeyNotFoundException>(() => _ = _data["extensions"]);
        }

        [Test]
        public void Data_AllowDecimals_Empty_Test()
        {
            Assert.Throws<KeyNotFoundException>(() => _ = _data["allowDecimals"]);
        }

        [Test]
        public void Data_MinValue_Empty_Test()
        {
            Assert.Throws<KeyNotFoundException>(() => _ = _data["minValue"]);
        }

        [Test]
        public void Data_MaxValue_Empty_Test()
        {
            Assert.Throws<KeyNotFoundException>(() => _ = _data["maxValue"]);
        }
    }
}