using Sin.Net.Domain.IO;
using Sin.Net.Domain.IO.Adapter;
using Sin.Net.Domain.IO.Settings;
using Sin.Net.Domain.Logging;
using Sin.Net.Persistence.IO;
using Sin.Net.Persistence.Settings;

namespace Sin.Net.Persistence.Imports
{
    internal class JsonImporter : IImportable
    {
        // -- fields

        private FileSetting _setting;

        string _importJson;

        // -- constructor

        public JsonImporter()
        {

        }

        // -- methods

        public IImportable Setup(SettingsBase setting)
        {
            if (setting is FileSetting)
            {
                _setting = setting as FileSetting;
            }
            else
            {
                Log.Error("The json import setting has the wrong type and was not accepted.");
            }
            return this;
        }

        public T ConvertWith<T>(IAdaptable adapter) where T : new()
        {
            return adapter.Adapt<string, T>(_importJson);
        }

        public IImportable Import()
        {
            _importJson = JsonIO.ReadJson(_setting.FullPath);
            return this;
        }

        public T Get<T>() where T : new()
        {
            return JsonIO.FromJsonString<T>(_importJson);
        }

        public T With<T>(IAdaptable adapter) where T : new()
        {
            return adapter.Adapt<string, T>(_importJson);
        }

        // -- properties

        public string Type => Persistence.Constants.Json.Key;

    }
}
