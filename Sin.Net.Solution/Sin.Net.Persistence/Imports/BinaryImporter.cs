using Sin.Net.Domain.IO;
using Sin.Net.Domain.IO.Adapter;
using Sin.Net.Domain.IO.Settings;
using Sin.Net.Domain.Logging;
using Sin.Net.Persistence.Settings;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

namespace Sin.Net.Persistence.Imports
{
    public class BinaryImporter : IImportable
    {
        // -- fields

        private FileSetting _setting;
        private object _data;

        // -- constructors 

        public BinaryImporter()
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
                Log.Error($"The setting has the wrong type and was not accepted.", this);
            }
            return this;
        }

        public IImportable Import()
        {
            Stream s = null;
            try
            {
                using (s = File.Open(_setting.FullPath, FileMode.Open))
                {
                    BinaryFormatter formatter = new BinaryFormatter();
                    _data = formatter.Deserialize(s);
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

            return this;
        }

        public T Get<T>() where T : new()
        {
            return (T)_data;
        }

        public T With<T>(IAdaptable adapter) where T : new()
        {
            return adapter.Adapt<object, T>(_data);
        }

        // -- properties

        public string Type => Constants.Binary.Key;
    }
}
