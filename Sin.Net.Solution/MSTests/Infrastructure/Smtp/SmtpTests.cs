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

        public void SendEmails()
        {
            // arrange
            var address = "";
            var num = 2;

            var config = new SmtpConfig("smtp.gmail.com");
            config.Port = 587;
            config.Credentials = new SmtpConfig.SmtpCredentials
            {
                Name = address,
                Password = ""
            };
            config.Sender = address;
            config.Receivers = Enumerable.Repeat(address, num).ToList();

            var subject = "Greetings from Sin.Net";
            var body = "Hello world";

            // act
            var smtp = new SmtpController();

            var result = smtp.Setup(config)
                .BuildMessage(subject, body)
                .Send();

            // assert
            Assert.IsTrue(result, "The smtp controller should have been successfull");
            Assert.IsTrue(smtp.Counter == num, $"The {smtp.GetType().Name} should have sent {num} emails.");
        }
    }
}
