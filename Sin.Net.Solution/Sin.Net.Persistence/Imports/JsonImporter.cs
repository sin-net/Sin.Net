using System;
using Sin.Net.Domain.Persistence;
using Sin.Net.Domain.Persistence.Adapter;
using Sin.Net.Domain.Persistence.Logging;
using Sin.Net.Domain.Persistence.Settings;
using Sin.Net.Persistence.IO.Json;
using Sin.Net.Persistence.Settings;

namespace Sin.Net.Persistence.Imports
{
	public class JsonImporter : ImporterBase
	{
		// -- fields

		private JsonSetting _setting;
		private string _importJson;

		// -- constructor

		public JsonImporter() : base()
		{

		}

		// -- methods

		public override IImportable Setup(SettingsBase setting)
		{
			try
			{
				_setting = setting as JsonSetting;
			}
			catch (Exception ex)
			{
				Log.Error("The json import setting has the wrong type and was not accepted.");
				base.HandleException(ex);
			}
			return this;
		}

		public override IImportable Import()
		{
			try
			{
				_importJson = JsonIO.ReadJson(_setting.FullPath);
			}
			catch (Exception ex)
			{
				base.HandleException(ex);
			}
			return this;
		}

		public override T As<T>()
		{
			try
			{
				JsonIO.Binder = _setting.Binder;
				JsonIO.Converters = _setting.Converters;
				JsonIO.Resolver = _setting.Resolver;
				return JsonIO.FromJsonString<T>(_importJson);
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
				return adapter.Adapt<string, T>(_importJson);
			}
			catch (Exception ex)
			{
				base.HandleException(ex);
			}
			return default;
		}

		public override object AsItIs()
		{
			return _importJson;
		}

		// -- properties

		public override string Type => Persistence.Constants.Json.Key;
	}
}
