using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sin.Net.Persistence.Settings
{
    public class JsonSetting : FileSetting
    {
        // -- constructor

        public JsonSetting()
        {

        }

        // -- properties

        public ISerializationBinder Binder { get; set; }
    }
}
