using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
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

        [TestMethod]
        public void SerializeToJson()
        {
            // arrange
            var input = new {
                MyName = "name",
                MyDate = DateTime.Now,
                MyState = States.Good
            };

            // act
            var json = JsonIO.ToJsonString(input, null, new List<JsonConverter> { new StringEnumConverter() });

            // assert
            Assert.IsTrue(!string.IsNullOrEmpty(json), "The json string should not be empty.");

            dynamic output = JsonIO.FromJsonString<object>(json);
        }
    }
}
