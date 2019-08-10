using Microsoft.VisualStudio.TestTools.UnitTesting;
using MSTests.Persistence.Data;
using Sin.Net.Domain.Persistence;
using Sin.Net.Domain.Persistence.Logging;
using Sin.Net.Persistence;
using Sin.Net.Persistence.IO;
using Sin.Net.Persistence.IO.Json;
using Sin.Net.Persistence.Settings;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;

namespace MSTests.Persistence
{
    [TestClass]
    public class PersistenceControllerTests : TestsBase
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
        public void ExportInterfacesAndImportJson()
        {
            // assert
            var input = new InterfaceRepository();
            input.Add(new TestClassA());
            input.Add(new TestClassB());
            input.Add(new TestClassA());
            input.Add(new TestClassA());

            var binder = new TypedSerializationBinder(new List<Type> {
                typeof(TestClassA),
                typeof(TestClassB)
            });

            string json = JsonIO.ToJsonString(input, binder);

            // act
            var output = JsonIO.FromJsonString<InterfaceRepository>(json, binder);

            // Assert
            for (int i = 0; i < output.Count; i++)
            {
                Assert.AreEqual(output[i].Name, input[i].Name, $"Name property at {i} is not equal");
                Assert.AreEqual(output[i].Id, input[i].Id, $"Id property at {i} is not equal");
            }
        }

        [TestMethod]
        public void ExportAndImportJson()
        {
            var setting = new JsonSetting { Location = _path, Name = "test-setting.json" };

            // act & assert export

            var result = _io.Exporter(Constants.Json.Key)
                .Setup(setting)
                .Build(setting)
                .Export();

            Assert.IsNotNull(result, "json export failed");

            // act & assert import

            var importedSetting = _io.Importer(Constants.Json.Key)
                .Setup(setting)
                .Import()
                .As<JsonSetting>();

            Assert.AreEqual(setting.Name, importedSetting.Name, "json import failed");
            Log.Info("json success", this);
        }

        [TestMethod]
        public void ExportAndImportBinary()
        {
            var name = $"test.{Constants.Binary.Extension}";
            var setting = new FileSetting { Location = _path, Name = name };

            var data = new Dictionary<string, object>();
            data.Add("string", "test-data");
            data.Add("int", 123);

            // act & assert export

            var result = _io.Exporter(Constants.Binary.Key)
                .Setup(setting)
                .Build(data)
                .Export();

            Assert.IsNotNull(result, "binary export failed");

            // act & assert import

            var importedData = _io.Importer(Constants.Binary.Key)
                .Setup(setting)
                .Import()
                .As<Dictionary<string, object>>();

            Assert.AreEqual(data["string"], importedData["string"], "binary import string key failed");
            Assert.AreEqual(data["int"], importedData["int"], "binary import int key failed");
            Log.Info("binary success", this);
        }

        [TestMethod]
        public void ExAndImportCsv()
        {
            // arrange
            var key = Constants.Csv.Key;
            var name = $"test-csv.{Constants.Csv.Extension}";
            var setting = new CsvSetting { Location = _path, Name = name, HasHeader = true };
            setting.CustomHeader = "#my custom line 1 \r\n#my custom line 2";
            setting.DataPosition = 3;

            var table = new DataTable("test-table");
            table.Columns.AddRange(new DataColumn[]
            {
                new DataColumn("col_1"),
                new DataColumn("col_2"),
                new DataColumn("col_3")
            });

            var rand = new Random();
            for (int i = 0; i < 10; i++)
            {
                table.Rows.Add(new object[] {
                    rand.Next(0, 10),
                    rand.Next(10, 100),
                    rand.NextDouble()
                });
            }

            // act export
            var result = _io.Exporter(key)
                .Setup(setting)
                .Build(table)
                .Export();

            // assert export
            Assert.IsNotNull(result, $"{key} export failed");
            setting.CustomHeader = "";

            // act import
            DataTable importedTable = _io.Importer(key)
                .Setup(setting)
                .Import().AsItIs() as DataTable;

            // assert import
            for (int i = 0; i < importedTable.Rows.Count; i++)
            {
                for (int j = 0; j < importedTable.Columns.Count; j++)
                {
                    Assert.AreEqual(
                        importedTable.Rows[i].ItemArray[j],
                        table.Rows[i].ItemArray[j],
                        $"row index {i} at item {j} failed");
                }
            }
        }
    }
}
