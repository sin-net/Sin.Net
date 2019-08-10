using Microsoft.VisualStudio.TestTools.UnitTesting;
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

        [TestMethod]
        public void SerializeToJson()
        {
            var input = new {
                MyName = "name",
                MyDate = DateTime.Now
            };

            JsonIO.ToJsonString(input);
        }
    }
}
