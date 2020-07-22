using Sin.Net.Domain.Exeptions;
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
    public class CsvImporter : ImporterBase
    {
        // -- fields

        private DataTable _table;
        private CsvSetting _setting;

        // -- constructor

        public CsvImporter() : base()
        {
            _setting = new CsvSetting
            {
                Separator = ';',
                HasHeader = true
            };
        }

        // -- methods

        public override IImportable Setup(SettingsBase setting)
        {
            try
			{
                _setting = setting as CsvSetting;
            }
            catch (Exception ex)
			{
                Log.Error("The csv import setting has the wrong type and was not accepted.");
                base.HandleException(ex);
            }

            return this;
        }

        /// <summary>
        /// Importiert die CSV-Datei entsprechend der eingestellten Parameter und legt diese intern als DataTable-Objekt ab.
        /// </summary>
        /// <returns>Die aufrufende Objektinstanz für das Method-Chaining</returns>
        public override IImportable Import()
        {
            try
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
            }
            catch (Exception ex)
			{
                base.HandleException(ex);
			}
            finally
			{

			}
                       
            return this;
        }

        public override T As<T>()
        {
            try
			{
                return (T)Convert.ChangeType(_table, typeof(T));
            }
            catch (Exception ex)
			{
                base.HandleException(ex);
			}
            return default;
        }

        public override T As<T>(IAdaptable adapter)
        {
            try
            {
                return adapter.Adapt<DataTable, T>(_table);
            }
            catch (Exception ex)
            {
                base.HandleException(ex);
            }
            return default;
        }

        public override object AsItIs()
        {
            return _table;
        }

        // -- properties

        public override string Type => Constants.Csv.Key;

	}
}