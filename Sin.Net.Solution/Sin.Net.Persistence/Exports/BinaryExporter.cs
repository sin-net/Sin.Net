using Sin.Net.Domain.Persistence;
using Sin.Net.Domain.Persistence.Adapter;
using Sin.Net.Domain.Persistence.Logging;
using Sin.Net.Domain.Persistence.Settings;
using Sin.Net.Persistence.IO;
using Sin.Net.Persistence.IO.Binary;
using Sin.Net.Persistence.Settings;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Sin.Net.Persistence.Exports
{
    public class BinaryExporter : IExportable
    {
        // -- fields

        private FileSetting _setting;
        private object _data;

        // -- constructor

        public BinaryExporter()
        {

        }

        public IExportable Setup(SettingsBase setting)
        {
            if (setting is FileSetting)
            {
                _setting = setting as FileSetting;
            }
            else
            {
                Log.Error($"The setting has the wrong type and was not accepted.", this);
            }
            return this;
        }


        public IExportable Build<T>(T data)
        {
            return Build(data, null);
        }

        public IExportable Build<T>(T data, IAdaptable adapter)
        {
            if (adapter == null)
            {
                _data = data;
            }
            else
            {
                _data = adapter.Adapt<T, object>(data);
            }
            return this;
        }

        public string Export()
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
                Log.Fatal(ex);
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

        public string Type => Constants.Binary.Key;

    }
}
