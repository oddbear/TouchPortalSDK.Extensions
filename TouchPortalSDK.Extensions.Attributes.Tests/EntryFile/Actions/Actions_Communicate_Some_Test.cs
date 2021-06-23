using System.Collections.Generic;
using NUnit.Framework;
using TouchPortalSDK.Extensions.Attributes;
using TouchPortalSDK.Extensions.Reflection.Contexts;
using CategoryAttribute = TouchPortalSDK.Extensions.Attributes.CategoryAttribute;

namespace TouchPortalSDK.Extensions.Reflection.Tests.EntryFile.Actions
{
    [Plugin]
    public class Actions_Communicate_Some_Test
    {
        private PluginContext _pluginContext;
        private string _pluginId;
        private Dictionary<string, object> _category;
        private Dictionary<string, object> _action;

        public enum Categories { [Category(Name="Category Name")] Category1 }

        [Attributes.Actions.Communicate(Category = nameof(Categories.Category1),
            Id = "actionId",
            Name = "Action Name",
            Prefix = "Prefix",
            Description = "Description",
            TryInline = true,
            Format = "Format",
            HasHoldFunctionality = true)]
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
            Assert.AreEqual("actionId", _action["id"]);
        }

        [Test]
        public void Action_Name_Empty_Test()
        {
            Assert.AreEqual("Action Name", _action["name"]);
        }

        [Test]
        public void Action_Prefix_Empty_Test()
        {
            Assert.AreEqual("Prefix", _action["prefix"]);
        }

        [Test]
        public void Action_Type_Empty_Test()
        {
            Assert.AreEqual("communicate", _action["type"]);
        }
        
        [Test]
        public void Action_ExecutionType_Set_Test()
        {
            Assert.False(_action.ContainsKey("executionType"));
        }

        [Test]
        public void Action_ExecutionCmd_Set_Test()
        {
            Assert.False(_action.ContainsKey("execution_cmd"));
        }

        [Test]
        public void Action_Description_Set_Test()
        {
            Assert.AreEqual("Description", _action["description"]);
        }

        [Test]
        public void Action_Format_Set_Test()
        {
            Assert.AreEqual("Format", _action["format"]);
        }

        [Test]
        public void Action_TryInline_Empty_Test()
        {
            Assert.AreEqual(true, _action["tryInline"]);
        }

        [Test]
        public void Action_HasHoldFunctionality_Empty_Test()
        {
            Assert.AreEqual(true, _action["hasHoldFunctionality"]);
        }
    }
}