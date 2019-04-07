using Microsoft.VisualStudio.TestTools.UnitTesting;
using Sin.Net.Domain.IO;
using Sin.Net.Domain.Logging;
using Sin.Net.Domain.Repository;
using Sin.Net.Persistence;
using Sin.Net.Persistence.Settings;
using System;
using System.IO;

namespace MSTests.Persistence
{
    [TestClass]
    public class RepositoryExportTests : TestBase
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
        public void ExportRepositoryToJson()
        {
            // arrange 
            var key = Constants.Json.Key;
            var setting = new FileSetting { Location = _path, Name = "test-repo.json" };
            var testRepo = new TestRepository { MyProperty = 4711 };
            var rand = new Random();

            for (int i = 0; i < 10; i++)
            {
                testRepo.Add(rand.NextDouble());
            }

            // act & assert export

            var result = _io.Exporter(key)
                .Setup(setting)
                .Build(new RepositoryView<double>().From(testRepo))
                .Export();

            Assert.IsNotNull(result, $"{key} export failed");

            // act & assert import

            // TODO: implement proper adaption from object into specific repository
            var importedRepo = _io.Importer(key)
                .Setup(setting)
                .Import()
                .Get<object>();

            //Assert.AreEqual(setting.Name, importedSetting.Name, $"{key} import failed");
            //Log.Info($"{key} success", this);
        }
    }
}
