using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Sin.Net.Domain.Exeptions;
using Sin.Net.Domain.Persistence;
using Sin.Net.Domain.Persistence.Adapter;
using Sin.Net.Domain.Persistence.Settings;
using Sin.Net.Persistence.IO.Binary;
using Sin.Net.Persistence.Settings;

namespace Sin.Net.Persistence.Exports
{
	public class BinaryExporter : ExporterBase
	{
		// -- fields

		private FileSetting _setting;
		private object _data;

		// -- constructor

		public BinaryExporter() : base()
		{
			
		}

		public override IExportable Setup(SettingsBase setting)
		{
			try
			{
				_setting = setting as FileSetting;
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

		public override IExportable Build<T>(T data)
		{
			return Build(data, null);
		}

		public override IExportable Build<T>(T data, IAdaptable adapter)
		{
			try
			{
				if (adapter == null)
				{
					_data = data;
				}
				else
				{
					_data = adapter.Adapt<T, object>(data);
				}
			}
			catch (Exception ex)
			{
				HandleException(ex);
			}
			finally
			{

			}

			return this;
		}

		public override string Export()
		{
			var s = default(Stream);
			var result = "";
			try
			{
				// restore path
				if (!string.IsNullOrEmpty(_setting.Location) &&
					!Directory.Exists(_setting.Location))
				{
					Directory.CreateDirectory(_setting.Location);
				}

				using (s = File.Open(_setting.FullPath, FileMode.Create))
				{
					BinaryFormatter formatter = new BinaryFormatter();
					formatter.Serialize(s, BinaryIO.ToBytes(_data));
					result = _setting.FullPath;
				}
			}
			catch (Exception ex)
			{
				HandleException(ex);
			}
			finally
			{
				if (s != null)
				{
					s.Close();
				}
			}
			return result;
		}
		
		// -- properties

		public override string Type => Constants.Binary.Key;
			
	}
}
