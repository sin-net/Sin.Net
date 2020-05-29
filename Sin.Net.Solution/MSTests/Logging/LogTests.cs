using Microsoft.VisualStudio.TestTools.UnitTesting;
using NLog;
using Sin.Net.Domain.Persistence.Logging;
using Sin.Net.Domain.System.UserManagement;
using Sin.Net.Logging;
using System;
using System.Collections.Generic;

namespace MSTests.Logging
{
    [TestClass]
    public class LogTests
    {
        [TestInitialize]
        public void Arrange()
        {
            Log.Inject(new NLogger { MinRule = LogLevel.Trace, Suffix = "-suffix" }.Start());
        }

        [TestCleanup]
        public void Cleanup()
        {
            // cleanup
        }

        // -- test methods

        [TestMethod]
        public void TestAttachment()
        {
            // pre-assert
            Assert.IsTrue(Log.IsNotNull, "logger should be injected");

            Log.Trace("demo logging with no attachment", null, null);
            Log.Trace("demo logging with attachment", null, new User { Name = "Test-User" });

            var list = new List<User> {
                new User { Name = "User 1" },
                new User { Name = "User 2" },
                new User { Name = "User 3" },
                new User { Name = "User 4" }
            };
            Log.Trace("demo logging with list attached", this, list);
            Log.Trace("demo logging with empty list attached", this, new List<int>());
            Log.Trace("demo logging with single entry list attached", this, new List<int> { 5 });

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
