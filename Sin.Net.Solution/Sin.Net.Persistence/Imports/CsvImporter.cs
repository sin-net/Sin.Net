using Sin.Net.Domain.IO;
using Sin.Net.Domain.IO.Adapter;
using Sin.Net.Domain.IO.Settings;
using Sin.Net.Domain.Logging;
using Sin.Net.Persistence.Settings;
using System;
using System.Data;
using System.IO;

namespace Sin.Net.Persistence.Imports
{
    /// <summary>
    /// Diese Klasse implementiert die IStrategyImportable Schnittstelle für den lesenden Zugriff auf CSV-Dateien
    /// </summary>
    class CsvImporter : IImportable
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
            string[] fields = lines[0].Split(new char[] { separator });
            int length = fields.GetLength(0);

            //1st row can be used for column names
            for (int i = 0; i < length; i++)
            {
                if (hasHeader == true)
                {
                    _table.Columns.Add(fields[i].ToLower());
                }
                else
                {
                    _table.Columns.Add(i.ToString());
                }
            }

            // all other rows
            DataRow Row;
            int firstRowIndex = (hasHeader == true ? 1 : 0);
            for (int i = firstRowIndex; i < lines.GetLength(0); i++)
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