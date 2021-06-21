using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using TouchPortalSDK.Extensions.Attributes;
using TouchPortalSDK.Extensions.Reflection.Contexts;
using CategoryAttribute = TouchPortalSDK.Extensions.Attributes.CategoryAttribute;

namespace TouchPortalSDK.Extensions.Reflection.Tests.EntryFile.States
{
    [Plugin]
    public class States_Some_Test
    {
        private PluginContext _pluginContext;
        private string _pluginId;
        private Dictionary<string, object> _category;
        private Dictionary<string, object> _state;

        public enum Categories {[Category(id: "category")] Category1 }

        [State(category: "category",
            id: "stateId",
            type:"text",
            desc:"desc",
            @default:"default",
            valueChoices: new []{ "test" })]
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
            Assert.AreEqual("stateId", _state["id"]);
        }

        [Test]
        public void State_Type_Empty_Test()
        {
            Assert.AreEqual("text", _state["type"]);
        }

        [Test]
        public void State_Desc_Empty_Test()
        {
            Assert.AreEqual("desc", _state["desc"]);
        }

        [Test]
        public void State_Default_Empty_Test()
        {
            Assert.AreEqual("default", _state["default"]);
        }

        [Test]
        public void State_ValueChoices_Empty_Test()
        {
            CollectionAssert.AreEquivalent(new[] { "test" }, (IEnumerable)_state["valueChoices"]);
        }
    }
}