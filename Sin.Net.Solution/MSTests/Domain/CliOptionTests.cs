using System;
using System.Collections.Generic;
using System.Globalization;
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
			var counter = 15;
			var flt = 1.5F;
			var dbl = 1.0000005;
			var args = new string[] {
				"--c", counter.ToString(),
				"--f", flt.ToString(),
				"--d", dbl.ToString(),
				"--p", "App_Data",
				"--o" };
			
			var parser = new CliOptionParser<CliDummyOptions>("--", CultureInfo.CurrentCulture);

			// act
			var options = parser.ReadArguments(args);

			// assert
			Assert.IsNotNull(options, "The options should not be null");
			Assert.IsTrue(options.Counter == counter, "Counter does not match.");
			Assert.IsTrue(options.Floating == flt, "Float does not match."); ;
			Assert.IsTrue(options.Double == dbl, "Double does not match.");
			Assert.IsFalse(options.Verbose, "Verbose should be false");
			Assert.IsTrue(!string.IsNullOrEmpty(options.Path), "Path sould be present");
			Assert.IsTrue(options.OnOff, "OnOff should be true.");
		}
	}
}
