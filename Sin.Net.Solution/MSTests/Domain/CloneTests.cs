using Microsoft.VisualStudio.TestTools.UnitTesting;
using Sin.Net.Domain.Infrastructure.Http;
using Sin.Net.Domain.System.Extensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace MSTests.Domain
{
    [TestClass]
    public class ConeTests : TestsBase
    {
        [TestInitialize]
        public override void Arrange()
        {
            base.Arrange();
        }

        [TestCleanup]
        public override void Cleanup()
        {
            base.Cleanup();
        }

        // -- test methods

        [TestMethod]
        public void Clone()
        {
            // arrange
            var data = new HttpEndpoint();
            data.BaseAddress = "47.11";

            // act
            var shadow = data;
            var clone = data.Clone();

            // assert
            Assert.AreEqual(data, shadow);
            Assert.AreNotEqual(shadow, clone);
            Assert.AreNotEqual(data, clone);
            Assert.AreEqual(data.BaseAddress, clone.BaseAddress);

            data.BaseAddress = "08/15";

            Assert.AreEqual(data.BaseAddress, shadow.BaseAddress);
            Assert.AreNotEqual(data.BaseAddress, clone.BaseAddress);
        }
    }
}
