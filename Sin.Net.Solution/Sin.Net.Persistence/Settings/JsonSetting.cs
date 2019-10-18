using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sin.Net.Persistence.Settings
{
    public class JsonSetting : FileSetting
    {
        // -- constructor

        public JsonSetting()
        {

        }

        private bool HasStringEnumConverter()
        {
            return Converters.Exists(c => typeof(StringEnumConverter).IsAssignableFrom(c.GetType()));
        }

        private JsonConverter GetStringEnumConverter()
        {
            var s = new JsonSerializerSettings();
            return Converters.FirstOrDefault(c => typeof(StringEnumConverter).IsAssignableFrom(c.GetType()));

            
        }

        // -- properties

        public ISerializationBinder Binder { get; set; }

        public List<JsonConverter> Converters { get; set; }

        public IContractResolver Resolver { get; set; }

        /// <summary>
        /// Sets the StringEnumConverter into the list or removes it, if present. 
        /// </summary>
        public bool ConvertEnumToString {
            set
            {
                if (Converters == null)
                {
                    Converters = new List<JsonConverter>();
                };
                if (value == true)
                {
                    if (!HasStringEnumConverter())
                    {
                        Converters.Add(new StringEnumConverter());
                    }
                }
                else
                {
                    if (HasStringEnumConverter())
                    {
                        Converters.Remove(GetStringEnumConverter());
                    }
                }
            }
        }
    }
}
