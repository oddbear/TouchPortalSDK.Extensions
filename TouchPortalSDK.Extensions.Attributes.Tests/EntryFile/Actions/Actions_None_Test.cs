using System.Collections.Generic;
using NUnit.Framework;
using TouchPortalSDK.Extensions.Attributes.Attributes;
using TouchPortalSDK.Extensions.Attributes.Reflection;
using TouchPortalSDK.Extensions.Attributes.Reflection.Contexts;
using Data = TouchPortalSDK.Extensions.Attributes.Attributes.DataAttribute;

namespace TouchPortalSDK.Extensions.Attributes.Tests.EntryFile.Actions
{
    [Plugin]
    public class Actions_None_Test
    {
        public enum Categories
        {
            [Attributes.Category]
            Category1
        }

        private PluginContext _pluginContext;
        private string _pluginId;
        private Dictionary<string, object> _category;
        private Dictionary<string, object> _action;

        [Action("Category1")]
        public void Action([Data]string strValue, [Data]int intValue)
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
            Assert.AreEqual(_pluginId + ".Category1.action." + nameof(Action), _action["id"]);
        }

        [Test]
        public void Action_Name_Empty_Test()
        {
            Assert.AreEqual(nameof(Action), _action["name"]);
        }

        [Test]
        public void Action_Format_Set_Test()
        {
            //TODO: What should be the default behaviour here? Need to test how Touch Portal interact with it. Should it be empty?
            Assert.Throws<KeyNotFoundException>(() => _ = _action["format"]);
        }
    }
}