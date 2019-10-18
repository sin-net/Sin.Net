using Sin.Net.Domain.Persistence;
using Sin.Net.Domain.Persistence.Adapter;
using Sin.Net.Domain.Persistence.Logging;
using Sin.Net.Domain.Persistence.Settings;
using Sin.Net.Persistence.IO.Json;
using Sin.Net.Persistence.Settings;

namespace Sin.Net.Persistence.Imports
{
    internal class JsonImporter : IImportable
    {
        // -- fields

        private JsonSetting _setting;
        private string _importJson;

        // -- constructor

        public JsonImporter()
        {

        }

        // -- methods

        public IImportable Setup(SettingsBase setting)
        {
            if (setting is JsonSetting)
            {
                _setting = setting as JsonSetting;
            }
            else
            {
                Log.Error("The json import setting has the wrong type and was not accepted.");
            }
            return this;
        }

        public IImportable Import()
        {
            _importJson = JsonIO.ReadJson(_setting.FullPath);
            return this;
        }

        public T As<T>() where T : new()
        {
            JsonIO.Binder = _setting.Binder;
            JsonIO.Converters = _setting.Converters;
            JsonIO.Resolver = _setting.Resolver;
            return JsonIO.FromJsonString<T>(_importJson);
        }

        public T As<T>(IAdaptable adapter) where T : new()
        {
            return adapter.Adapt<string, T>(_importJson);
        }

        public object AsItIs()
        {
            return _importJson;
        }

        // -- properties

        public string Type => Persistence.Constants.Json.Key;

    }
}
