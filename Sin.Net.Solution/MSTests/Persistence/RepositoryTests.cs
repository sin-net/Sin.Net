using Microsoft.VisualStudio.TestTools.UnitTesting;
using MSTests.Persistence.Data;
using Sin.Net.Domain.IO;
using Sin.Net.Domain.Logging;
using Sin.Net.Domain.Repository;
using Sin.Net.Persistence;
using Sin.Net.Persistence.IO;
using Sin.Net.Persistence.Settings;
using System;
using System.IO;

namespace MSTests.Persistence
{
    [TestClass]
    public class RepositoryTests : TestBase
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
        public void ExportRepositoryToByteArray()
        {
            // arrange
            var input = new TestRepository { MyProperty = "hello test" };
            
            // act
            var bytes = BinaryIO.ToBytes(input);
            var result = BinaryIO.FromBytes<TestRepository>(bytes);

            // assert
            Assert.AreEqual(result.MyProperty, input.MyProperty);

        }

        [TestMethod]
        public void ExportRepositoryToJson()
        {
            // arrange 
            var key = Constants.Json.Key;
            var setting = new FileSetting { Location = _path, Name = "test-repo.json" };
            var testRepo = new TestRepository { MyProperty = "hello test" };
            var rand = new Random();

            for (int i = 0; i < 10; i++)
            {
                testRepo.Add(rand.NextDouble());
            }

            // act & assert export

            var result = _io.Exporter(key)
                .Setup(setting)
                .Build(testRepo)
                .Export();

            Assert.IsNotNull(result, $"{key} export failed");

            // act & assert import

            var importedRepo = _io.Importer(key)
                .Setup(setting)
                .Import()
                .Get<TestRepository>();

            Assert.AreEqual(testRepo.MyProperty, importedRepo.MyProperty, $"{key} import failed");
            Log.Info($"{key} success", this);
        }

        [TestMethod]
        public void ExportIterativeRepositoryToJson()
        {
            // arrange 
            var key = Constants.Json.Key;
            var setting = new FileSetting { Location = _path, Name = "iterative-test-repo.json" };
            var testRepo = new IterativeTestRepository { MyProperty = 4711 };
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

            var importedRepo = _io.Importer(key)
                .Setup(setting)
                .Import()
                .Get<RepositoryView<double>>()
                .To<IterativeTestRepository>();

            Assert.AreEqual(testRepo.MyProperty, importedRepo.MyProperty, $"{key} import failed");
            Log.Info($"{key} success", this);
        }
    }
}
