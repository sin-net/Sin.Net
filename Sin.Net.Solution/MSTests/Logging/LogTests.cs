using Microsoft.VisualStudio.TestTools.UnitTesting;
using NLog;
using Sin.Net.Domain.Persistence.Logging;
using Sin.Net.Logging;
using System;

namespace MSTests.Logging
{
    [TestClass]
    public class LogTests
    {
        [TestInitialize]
        public void Arrange()
        {
            Log.Inject(new NLogger { MinRule = LogLevel.Warn, Suffix = "-suffix" }.Start());
        }

        [TestCleanup]
        public void Cleanup()
        {
            // cleanup
        }

        [TestMethod]
        public void TestNLogger()
        {
            // pre-assert
            Assert.IsTrue(Log.IsNotNull, "logger should be injected");

            // act
            Log.Trace("eine Trace Nachricht");
            Log.Debug("eine Debug Nachricht");
            Log.Info("eine Info Nachricht");
            Log.Warn("eine Warn Nachricht");
            Log.Error("eine Error Nachricht");

            try
            {
                Throw();
            }
            catch (Exception ex)
            {
                Log.Fatal(ex);
            }
        }

        private void Throw()
        {
            try
            {
                ThrowInner();
            }
            catch (Exception ex)
            {
                throw new Exception("An UnitTest exception was thrown", ex);
            }
        }

        private void ThrowInner()
        {
            throw new Exception("throw inner ex");
        }

    }
}
