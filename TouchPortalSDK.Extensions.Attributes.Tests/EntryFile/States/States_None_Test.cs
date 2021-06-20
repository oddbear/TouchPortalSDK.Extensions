using System.Collections.Generic;
using NUnit.Framework;
using TouchPortalSDK.Extensions.Attributes.Attributes;
using TouchPortalSDK.Extensions.Attributes.Reflection;
using TouchPortalSDK.Extensions.Attributes.Reflection.Contexts;

namespace TouchPortalSDK.Extensions.Attributes.Tests.EntryFile.States
{
    [Plugin]
    public class States_None_Test
    {
        private PluginContext _pluginContext;
        private string _pluginId;
        private Dictionary<string, object> _category;
        private Dictionary<string, object> _state;

        [State]
        public string State { get; set; }

        [SetUp]
        public void Setup()
        {
            _pluginContext = PluginTreeBuilder.Build(this.GetType());
            _pluginId = _pluginContext.GetId();

            var entryFile = EntryFileBuilder.BuildEntryFile(_pluginContext);
            var categories = (List<object>)entryFile["categories"];
            _category = (Dictionary<string, object>)categories[0];
            var states = (List<object>)_category["states"];
            _state = (Dictionary<string, object>)states[0];
        }

        [Test]
        public void State_Id_Empty_Test()
        {
            Assert.AreEqual(_pluginId + ".DefaultCategory.state." + nameof(State), _state["id"]);
        }

        [Test]
        public void State_Type_Empty_Test()
        {
            Assert.AreEqual("choice", _state["type"]);
        }

        [Test]
        public void State_Desc_Empty_Test()
        {
            Assert.AreEqual(nameof(State), _state["desc"]);
        }

        [Test]
        public void State_Default_Empty_Test()
        {
            Assert.AreEqual(string.Empty, _state["default"]);
        }

        [Test]
        public void State_ValueChoices_Empty_Test()
        {
            Assert.Throws<KeyNotFoundException>(() => _ = _state["valueChoices"]);
        }
    }
}