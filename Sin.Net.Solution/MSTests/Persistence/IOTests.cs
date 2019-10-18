using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using Sin.Net.Persistence.IO;
using Sin.Net.Persistence.IO.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace MSTests.Persistence
{
    [TestClass]
    public class IOTests
    {
        enum States {
            Unknown,
            Good,
            Bad
        }

        class Alien
        {
            public bool Invisible { get; set; }
            public string Name { get; set; }
            public DateTime Landed { get; set; }
            public States Feeling { get; set; }
        }
           

        [TestMethod]
        public void SerializeToJson()
        {
            // arrange
            var input = new Alien {
                Invisible = true,
                Name = "Alf",
                Landed = DateTime.Now,
                Feeling = States.Good
            };
            var resolver = new PropertiesContractResolver();
            resolver.IgnoreProperty(typeof(Alien), "Invisible");
            resolver.RenameProperty(typeof(Alien), "Name", "Alien");

            // act
            JsonIO.Resolver = resolver;
            JsonIO.Converters = new List<JsonConverter> { new StringEnumConverter() };
            var json = JsonIO.ToJsonString(input);

            // assert
            Assert.IsTrue(!string.IsNullOrEmpty(json), "The json string should not be empty.");
            Assert.IsTrue(!json.Contains("Invisible"), "The property Invisible should be invisible.");
            Assert.IsTrue(!json.Contains("Name"), "The property 'Name' should be renamed into 'AlienName'.");
            Assert.IsTrue(json.Contains("Alien"), "The property 'Name' should be renamed into 'Alien'.");
        }
    }
}
