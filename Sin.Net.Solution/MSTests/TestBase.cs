using Microsoft.VisualStudio.TestTools.UnitTesting;
using Sin.Net.Domain.Persistence.Logging;
using Sin.Net.Logging;

namespace MSTests
{
    [TestClass]
    public class TestsBase
    {
        [TestInitialize]
        public virtual void Arrange()
        {
            if (Log.IsNotNull == false)
            {
                Log.Inject(new TestLogger().Start());
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
