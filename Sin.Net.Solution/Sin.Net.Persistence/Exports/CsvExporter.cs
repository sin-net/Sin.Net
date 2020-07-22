using Sin.Net.Domain.Exeptions;
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

    public class CsvExporter : ExporterBase
    {

        // -- fields

        private CsvSetting _setting;
        private object _exportData;
        private DataTable _exportTable;

        // -- constructors

        public CsvExporter() : base()
        {

        }

        // -- methods

        public override IExportable Setup(SettingsBase setting)
        {
            try
			{
                _setting = setting as CsvSetting;
            }
            catch (Exception ex)
			{
                Log.Error("The csv export setting has the wrong type and was not accepted.");
                base.HandleException(ex);
			}
            finally
			{

			}

            return this;
        }

        /// <summary>
        /// Bindet die Daten für den Export ohne Konvertierung an.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        /// <returns></returns>
        public override IExportable Build<T>(T data)
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
        public override IExportable Build<T>(T data, IAdaptable adapter)
        {
            try
			{
                _exportData = data;
                _exportTable = adapter.Adapt<T, DataTable>(data);
                _exportTable.TableName = _setting.Name;
            }
            catch(Exception ex)
			{
                base.HandleException(ex);
			}
			finally
			{

			}
           
            return this;
        }


        public override string Export()
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
                base.HandleException(ex);
            }
            finally
            {
                csv = null; // resource is garbage collected after try block
            }

            return result;
        }

        // -- properties

        public override string Type => Constants.Csv.Key;

    }
}