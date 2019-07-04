using Sin.Net.Domain.Persistence;
using Sin.Net.Domain.Persistence.Adapter;
using Sin.Net.Domain.Persistence.Logging;
using Sin.Net.Domain.Persistence.Settings;
using Sin.Net.Persistence.Settings;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;

namespace Sin.Net.Persistence.Exports
{
    // TODO: english comments

    class CsvExporter : IExportable
    {

        // -- fields

        private CsvSetting _setting;
        private object _exportData;
        private DataTable _exportTable;

        // -- constructors

        public CsvExporter()
        {

        }

        // -- methods

        public IExportable Setup(SettingsBase setting)
        {
            if (setting is CsvSetting)
            {
                _setting = setting as CsvSetting;
            }
            else
            {
                Log.Error("The csv export setting has the wrong type and was not accepted.");
            }
            return this;
        }

        /// <summary>
        /// Bindet die Daten für den Export ohne Konvertierung an.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        /// <returns></returns>
        public IExportable Build<T>(T data)
        {
            _exportData = data;

            if (typeof(T).IsAssignableFrom(typeof(DataTable)) == true)
            {
                _exportTable = data as DataTable;
            }

            return this;
        }

        /// <summary>
        /// Bindet die Daten für den Export an.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        /// <param name="adapter"></param>
        /// <returns></returns>
        public IExportable Build<T>(T data, IAdaptable adapter)
        {
            _exportData = data;
            _exportTable = adapter.Adapt<T, DataTable>(data);
            _exportTable.TableName = _setting.Name;
            return this;
        }


        public string Export()
        {
            StringBuilder csv;
            string result = string.Empty;
            string seperator = _setting.Separator.ToString();

            try
            {
                // restore path
                if (!string.IsNullOrEmpty(_setting.Location) &&
                    !Directory.Exists(_setting.Location))
                {
                    Directory.CreateDirectory(_setting.Location);
                }

                csv = new StringBuilder();

                // csv header - optional
                if (string.IsNullOrEmpty(_setting.CustomHeader) == false)
                {
                    csv.AppendLine(_setting.CustomHeader);
                }

                // column names header - optional
                if (_setting.HasHeader)
                {
                    // csv content
                    IEnumerable<string> columnNames = _exportTable.Columns.Cast<DataColumn>().
                                                      Select(column => column.ColumnName);
                    csv.AppendLine(string.Join(seperator, columnNames));
                }

                // append data
                foreach (DataRow row in _exportTable.Rows)
                {
                    IEnumerable<string> fields = row.ItemArray.Select(field => field.ToString());
                    csv.AppendLine(string.Join(seperator, fields));
                }

                // flush to file
                File.WriteAllText(_setting.FullPath, csv.ToString());
                result = _setting.FullPath;
            }
            catch (Exception ex)
            {
                Log.Error($"An error occured during csv export: {ex.Message}");
            }
            finally
            {
                csv = null; // resource is garbage collected after try block
            }

            return result;
        }

        // -- properties

        public string Type => Constants.Csv.Key;
    }
}