using System.Collections.Generic;
using NUnit.Framework;
using TouchPortalSDK.Extensions.Reflection;
using TouchPortalSDK.Extensions.Reflection.Contexts;

namespace TouchPortalSDK.Extensions.Attributes.Tests.EntryFile.Actions
{
    [Plugin]
    public class Actions_None_Test
    {
        private PluginContext _pluginContext;
        private string _pluginId;
        private Dictionary<string, object> _category;
        private Dictionary<string, object> _action;

        [Action]
        public void Action()
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
        }

        [Test]
        public void Action_Id_Empty_Test()
        {
            Assert.AreEqual(_pluginId + ".DefaultCategory.action." + nameof(Action), _action["id"]);
        }

        [Test]
        public void Action_Name_Empty_Test()
        {
            Assert.AreEqual(nameof(Action), _action["name"]);
        }

        [Test]
        public void Action_Prefix_Empty_Test()
        {
            Assert.AreEqual(string.Empty, _action["prefix"]);
        }

        [Test]
        public void Action_Type_Empty_Test()
        {
            Assert.AreEqual("communicate", _action["type"]);
        }
        
        [Test]
        public void Action_ExecutionType_Set_Test()
        {
            Assert.Throws<KeyNotFoundException>(() => _ = _action["executionType"]);
        }

        [Test]
        public void Action_ExecutionCmd_Set_Test()
        {
            Assert.Throws<KeyNotFoundException>(() => _ = _action["execution_cmd"]);
        }

        [Test]
        public void Action_Description_Set_Test()
        {
            //TODO: This rule might change to generate a default in the future:
            Assert.Throws<KeyNotFoundException>(() => _ = _action["description"]);
        }

        [Test]
        public void Action_Format_Set_Test()
        {
            //TODO: This rule might change to generate a default in the future:
            Assert.Throws<KeyNotFoundException>(() => _ = _action["format"]);
        }

        [Test]
        public void Action_TryInline_Empty_Test()
        {
            Assert.AreEqual(false, _action["tryInline"]);
        }

        [Test]
        public void Action_HasHoldFunctionality_Empty_Test()
        {
            Assert.AreEqual(false, _action["hasHoldFunctionality"]);
        }
    }
}