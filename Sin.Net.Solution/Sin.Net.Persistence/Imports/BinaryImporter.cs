using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Sin.Net.Domain.Exeptions;
using Sin.Net.Domain.Persistence;
using Sin.Net.Domain.Persistence.Adapter;
using Sin.Net.Domain.Persistence.Logging;
using Sin.Net.Domain.Persistence.Settings;
using Sin.Net.Persistence.IO.Binary;
using Sin.Net.Persistence.Settings;

namespace Sin.Net.Persistence.Imports
{
	public class BinaryImporter : ImporterBase
	{
		// -- fields

		private FileSetting _setting;
		private object _data;

		// -- constructors 

		public BinaryImporter() : base()
		{

		}

		// -- methods

		public override IImportable Setup(SettingsBase setting)
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

		public override IImportable Import()
		{
			Stream s = null;
			try
			{
				using (s = File.Open(_setting.FullPath, FileMode.Open))
				{
					BinaryFormatter formatter = new BinaryFormatter();
					byte[] bytes = (byte[])formatter.Deserialize(s);
					_data = BinaryIO.FromBytes<object>(bytes);
				}
			}
			catch (Exception ex)
			{
				base.HandleException(ex);
			}
			finally
			{
				if (s != null)
				{
					s.Close();
				}
			}

			return this;
		}

		public override T As<T>()
		{
			try
			{
				return (T)_data;
			}
			catch (Exception ex)
			{
				HandleException(ex);
			}

			return default;
		}

		public override T As<T>(IAdaptable adapter)
		{
			try
			{
				return adapter.Adapt<object, T>(_data);
			}
			catch (Exception ex)
			{
				HandleException(ex);
			}

			return default;
		}

		public override object AsItIs()
		{
			return _data;
		}

		// -- properties

		public override string Type => Constants.Binary.Key;
	}
}
