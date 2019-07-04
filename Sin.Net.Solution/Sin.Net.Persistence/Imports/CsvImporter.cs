using Sin.Net.Domain.Persistence;
using Sin.Net.Domain.Persistence.Adapter;
using Sin.Net.Domain.Persistence.Logging;
using Sin.Net.Domain.Persistence.Settings;
using Sin.Net.Persistence.Settings;
using System;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;

namespace Sin.Net.Persistence.Imports
{
    /// <summary>
    /// Diese Klasse implementiert die IStrategyImportable Schnittstelle für den lesenden Zugriff auf CSV-Dateien
    /// </summary>
    internal class CsvImporter : IImportable
    {
        // -- fields

        private DataTable _table;
        private CsvSetting _setting;

        // -- constructor

        public CsvImporter()
        {
            _setting = new CsvSetting
            {
                Separator = ';',
                HasHeader = true
            };
        }

        // -- methods

        public IImportable Setup(SettingsBase setting)
        {
            if (setting is CsvSetting)
            {
                _setting = setting as CsvSetting;
            }
            else
            {
                Log.Error("The csv import setting has the wrong type and was not accepted.");
            }
            return this;
        }

        /// <summary>
        /// Importiert die CSV-Datei entsprechend der eingestellten Parameter und legt diese intern als DataTable-Objekt ab.
        /// </summary>
        /// <returns>Die aufrufende Objektinstanz für das Method-Chaining</returns>
        public IImportable Import()
        {
            var separator = _setting.Separator;
            var hasHeader = _setting.HasHeader;

            _table = new DataTable();
            _table.TableName = _setting.Name;

            string[] lines = File.ReadAllLines(_setting.FullPath);

            string[] fields;
            if (hasHeader == true)
            {
                _table.Columns.AddRange(
                    lines[_setting.DataPosition - 1]
                    .ToLower()
                    .Split(new char[] { separator })
                    .Select(s => new DataColumn(s))
                    .ToArray());
            }
            else
            {
                _table.Columns.AddRange(
                    lines[_setting.DataPosition]
                    .Split(new char[] { separator })
                    .Select((s, i) => new DataColumn(i.ToString()))
                    .ToArray());
            }

            // read header
            var sb = new StringBuilder();
            lines.Take(_setting.DataPosition).ToList()
                .ForEach(s => sb.AppendLine(s));
            _setting.CustomHeader = sb.ToString();

            // all other rows
            DataRow Row;
            for (int i = _setting.DataPosition; i < lines.GetLength(0); i++)
            {
                fields = lines[i].Split(new char[] { separator });
                Row = _table.NewRow();
                for (int f = 0; f < fields.Length; f++)
                {
                    Row[f] = fields[f];

                }
                _table.Rows.Add(Row);
            }

            Log.Info($"csv import successfull for '{_setting.Name}'");
            return this;
        }

        public T As<T>() where T : new()
        {
            return (T)Convert.ChangeType(_table, typeof(T));
        }

        public T As<T>(IAdaptable adapter) where T : new()
        {
            return adapter.Adapt<DataTable, T>(_table);
        }

        public object AsItIs()
        {
            return _table;
        }

        // -- properties

        public string Type => Constants.Csv.Key;
    }
}