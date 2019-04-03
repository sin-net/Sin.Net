using Microsoft.VisualStudio.TestTools.UnitTesting;
using Sin.Net.Domain.Logging;
using Sin.Net.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace MSTests
{
    [TestClass]
    public class TestBase
    {
        [TestInitialize]
        public virtual void Arrange()
        {
            if (Log.IsNotNull == false)
            {
                Log.Inject(new TestLogger());
            }
        }

        [TestCleanup]
        public virtual void Cleanup()
        {
            Log.Stop();
        }

        // -- properties

        public string TestDataDirectory => "Test_Data";

    }
}
