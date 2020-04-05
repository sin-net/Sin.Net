using Microsoft.VisualStudio.TestTools.UnitTesting;
using Sin.Net.Infrastructure.Smtp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MSTests.Infrastructure.Smtp
{
    [TestClass]
    public class SmtpTests : TestsBase
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

        // test methods

        [TestMethod]
        public void SendEmails()
        {
            // arrange
            var address = "adriansinger87@gmail.com";
            var num = 2;

            var config = new SmtpConfig("smtp.gmail.com");
            config.User = address;
            config.Password = "";
            config.Subject = "Greetings from Sin.Net";
            config.Port = 587;
            config.Receivers = Enumerable.Repeat(address, num).ToList();
            config.Body = "Hello world";
            // act
            var smtp = new SmtpController().Setup(config).Send();


            // assert
            Assert.IsTrue(smtp.Counter == num, $"The {smtp.GetType().Name} should have sent {num} emails.");
        }
    }
}
