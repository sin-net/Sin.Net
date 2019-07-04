using Microsoft.VisualStudio.TestTools.UnitTesting;
using Sin.Net.Domain.Enumerations;
using Sin.Net.Domain.Persistence.Logging;
using System;

namespace MSTests.Security
{
    [TestClass]
    public class SecurityTests : TestsBase
    {
        private SecurityAccess _tester;

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
        public void AccessLevelOne()
        {
            // arrange
            _tester = new SecurityAccess((int)SecurityLevels.User);

            // act
            try
            {
                _tester.AccessLevelOne();
            }
            catch (Exception ex)
            {
                Log.Fatal(ex);
                _tester.Passed = false;
            }


            // assert
            Assert.IsTrue(_tester.Passed);
        }

        [TestMethod]
        public void AccessLevelTwo()
        {
            // arrange
            _tester = new SecurityAccess((int)SecurityLevels.User);

            // act
            try
            {
                _tester.AccessLevelTwo();
            }
            catch (Exception ex)
            {
                Log.Fatal(ex);
                _tester.Passed = false;
            }

            // assert
            Assert.IsFalse(_tester.Passed);
        }

        [TestMethod]
        public void AccessProperty()
        {
            // arrange
            var myFloat = 5.5F;
            _tester = new SecurityAccess((int)SecurityLevels.Guest);

            // act
            try
            {
                _tester.SomeFloat = myFloat;
            }
            catch (Exception ex)
            {
                Log.Fatal(ex);
            }

            // assert
            Assert.IsFalse(_tester.Passed);


            // arrange
            _tester = new SecurityAccess((int)SecurityLevels.Expert);

            // act
            try
            {
                _tester.SomeFloat = myFloat;
            }
            catch (Exception ex)
            {
                Log.Fatal(ex);
            }

            // assert
            Assert.IsTrue(_tester.Passed);
            Assert.AreEqual(myFloat, _tester.SomeFloat);
        }
    }
}
