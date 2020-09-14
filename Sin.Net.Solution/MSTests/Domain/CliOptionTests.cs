using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Sin.Net.Domain.Cli;

namespace MSTests.Domain
{
	[TestClass]
	public class CliOptionTests : TestsBase
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

		// -- Test methods

		[TestMethod]
		public void TestCliAttributes()
		{
			// arrange
			var args = new string[] { "-verbose", "-p", "App_Data", "-o" };
			
			var parser = new CliOptionParser<CliDummyOptions>();

			// act
			var options = parser.ReadArguments(args);

			// assert
			Assert.IsNotNull(options, "The options should not be null");
			Assert.IsTrue(options.Verbose, "Verbose should be true");
			Assert.IsTrue(!string.IsNullOrEmpty(options.Path), "Path sould be present");
			Assert.IsTrue(options.OnOff, "Options OnOff should be true");
		}
	}
}
