using Microsoft.VisualStudio.TestTools.UnitTesting;
using Sin.Net.Domain.IO;
using Sin.Net.Domain.Logging;
using Sin.Net.Persistence;
using Sin.Net.Persistence.Settings;
using System;
using System.IO;
using System.Reflection;

namespace MSTests.Persistence
{
    [TestClass]
    public class PersistenceControllerTests : TestBase
    {
        private IPersistenceControlable _io;
        private string _path;

        [TestInitialize]
        public override void Arrange()
        {
            base.Arrange();

            _path = Path.Combine(Environment.CurrentDirectory, base.TestDataDirectory);
            _io = new PersistenceController();
        }

        [TestCleanup]
        public override void Cleanup()
        {
            base.Cleanup();
        }

        // -- Tests

        [TestMethod]
        public void ImportAndExportJson()
        {
            var setting = new FileSetting { Location = _path, Name = "test-setting.json" };

            // act & assert export

            var result = _io.Exporter(Constants.Json.Key)
                .Setup(setting)
                .Build(setting)
                .Export();

            Assert.IsNotNull(result, "json export failed");

            // act & assert import

            var importedSetting =_io.Importer(Constants.Json.Key)
                .Setup(setting)
                .Import()
                .Get<FileSetting>();

            Assert.AreEqual(setting.Name, importedSetting.Name, "json import failed");
            Log.Info("success", this);
        }
    }
}
