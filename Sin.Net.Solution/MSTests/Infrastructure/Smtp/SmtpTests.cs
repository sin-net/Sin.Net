using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Sin.Net.Domain.System.Security;
using Sin.Net.Infrastructure.Smtp;

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
			var address = "123@456.de";

			var num = 2;

			var config = new SmtpConfig("smtp.gmail.com")
			{
				Port = 587,
				Credentials = new SimpleCredentials(address, "password"),
				Sender = address,
				Receivers = Enumerable.Repeat(address, num).ToList()
			};

			var subject = "Greetings from Sin.Net";
			var body = "Hello world";

			// act
			var smtp = new SmtpController();

			// TODO: test attachment functions
			var result = smtp.Setup(config)
				.BuildMessage(subject, body)
				.Attach("App_Data/demo.csv")
				.Send();

			// assert
			Assert.IsTrue(result, "The smtp controller should have been successfull");
			Assert.IsTrue(smtp.Counter == num, $"The {smtp.GetType().Name} should have sent {num} emails.");
		}
	}
}
