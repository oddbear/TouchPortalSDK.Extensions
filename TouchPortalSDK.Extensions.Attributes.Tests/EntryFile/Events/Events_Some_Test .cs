using System.Collections.Generic;
using NUnit.Framework;
using TouchPortalSDK.Extensions.Attributes;
using TouchPortalSDK.Extensions.Reflection.Contexts;

namespace TouchPortalSDK.Extensions.Reflection.Tests.EntryFile.Events
{
    [Plugin]
    public class Events_Some_Test
    {
        private PluginContext _pluginContext;
        private string _pluginId;
        private Dictionary<string, object> _category;
        private Dictionary<string, object> _event;

        [Attributes.States.Text(Id = "stateId")]
        [Attributes.Events.Communicate(Id = "eventId",
            Name = "Event Name",
            Format = "Format",
            ValueChoices = new [] { "test" },
            ValueType = "dummy2")]
        public string State { get; set; }

        [SetUp]
        public void Setup()
        {
            _pluginContext = PluginTreeBuilder.Build(this.GetType());
            _pluginId = _pluginContext.GetId();

            var entryFile = EntryFileBuilder.BuildEntryFile(_pluginContext);
            var categories = (List<object>)entryFile["categories"];
            _category = (Dictionary<string, object>)categories[0];
            var events = (List<object>)_category["events"];
            _event = (Dictionary<string, object>)events[0];
        }

        [Test]
        public void Event_Id_Empty_Test()
        {
            Assert.AreEqual("eventId", _event["id"]);
        }

        [Test]
        public void Event_Name_Empty_Test()
        {
            Assert.AreEqual("Event Name", _event["name"]);
        }

        [Test]
        public void Event_Format_Empty_Test()
        {
            Assert.AreEqual("Format", _event["format"]);
        }

        [Test]
        public void Event_Type_Empty_Test()
        {
            Assert.AreEqual("communicate", _event["type"]);
        }
        
        [Test]
        public void Event_ValueChoices_Empty_Test()
        {
            Assert.AreEqual(new[] { "test" }, _event["valueChoices"]);
        }

        [Test]
        public void Event_ValueType_Empty_Test()
        {
            Assert.AreEqual("dummy2", _event["valueType"]);
        }

        [Test]
        public void Event_ValueStateId_Empty_Test()
        {
            Assert.AreEqual("stateId", _event["valueStateId"]);
        }
    }
}