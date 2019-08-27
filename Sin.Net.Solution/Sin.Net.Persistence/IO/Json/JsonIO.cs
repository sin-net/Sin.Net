using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Sin.Net.Domain.Persistence.Logging;
using System;
using System.Collections.Generic;
using System.IO;

namespace Sin.Net.Persistence.IO.Json
{

    /// <summary>
    /// Static class to convert between objects and json-strings. 
    /// </summary>
    public static class JsonIO
    {

        #region ToJsonString (2)

        /// <summary>
        /// Creates a json-string out of an object with a custom binder, usefull for interface serializations.
        /// </summary>
        /// <param name="obj">The objekt to be serialized.</param>
        /// <param name="binder">The optional implementation of an ISerializationBinder, e.g. TypedSerializationBinder</param>
        ///  <param name="converters">The optional list of json converters.</param>
        /// <returns>The resulting string<</returns>
        public static string ToJsonString(object obj, ISerializationBinder binder = null, List<JsonConverter> converters = null)
        {
            string json = "{ }";
            try
            {
                var settings = new JsonSerializerSettings
                {
                    TypeNameHandling = TypeNameHandling.Auto,
                    ContractResolver = new LowerCaseContractResolver(),
                    SerializationBinder = binder,
                    Converters = converters
                };

                json = JsonConvert.SerializeObject(obj, Formatting.Indented, settings);
            }
            catch (Exception ex)
            {
                handleException(ex);
                json = "{method: 'ToJsonString', error: '" + ex.Message + "'}";
            }
            return json;
        }
        #endregion


        /// <summary>
        /// Converts a json-stringinto an objekt of type T
        /// </summary>
        /// <typeparam name="T">The typeparam</typeparam>
        /// <param name="json">The json-string</param>
        /// <param name="binder">The optional binder implementation</param>
        /// <param name="converters">The optional list of json converters.</param>
        /// <returns>The deserialized object of type t</returns>
        public static T FromJsonString<T>(string json, ISerializationBinder binder = null, List<JsonConverter> converters = null)
        {
            try
            {
                var settings = new JsonSerializerSettings
                {
                    TypeNameHandling = TypeNameHandling.Auto,
                    SerializationBinder = binder,
                    Converters = converters
                };

                return JsonConvert.DeserializeObject<T>(json, settings);
            }
            catch (Exception ex)
            {
                handleException(ex);
                return default(T);
            }
        }

        /// <summary>
        /// Reads a json file and returns the complete json-string.
        /// </summary>
        /// <param name="file">The complete file path.</param>
        /// <returns>The deserialized object of type t.</returns>
        public static string ReadJson(string file)
        {
            string result = string.Empty;
            using (StreamReader r = new StreamReader(file))
            {
                result = r.ReadToEnd();
            }

            return result;
        }

        /// <summary>
        /// Reads a json file and returns the converted object.
        /// </summary>
        /// <typeparam name="T">The typeparam</typeparam>
        /// <param name="file">The complete file path.</param>
        /// <param name="binder">The optional binder implementation.</param>
        /// <param name="converters">The optional list of json converters.</param>
        /// <returns>The deserialized object of type t.</returns>
        public static T ReadJson<T>(string file, ISerializationBinder binder = null, List<JsonConverter> converters = null)
        {
            T obj;
            using (StreamReader r = new StreamReader(file))
            {
                string json = r.ReadToEnd();
                obj = FromJsonString<T>(json, binder, converters);
            }

            return obj;
        }

        /// <summary>
        /// Writes an serialized object into a file.
        /// </summary>
        /// <param name="obj">The object to be serialized.</param>
        /// <param name="file">The complete file path.</param>
        /// <param name="binder">The optional binder implementation.</param>
        /// <param name="converters">The optional list of json converters.</param>
        /// <returns>If successful it returns 'true' otherwise 'false'.</returns>
        public static bool SaveToJson(object obj, string file, ISerializationBinder binder = null, List<JsonConverter> converters = null)
        {
            bool result = false;
            try
            {
                File.WriteAllText(file, ToJsonString(obj, binder, converters));
                result = true;
            }
            catch (Exception ex)
            {
                handleException(ex);
            }
            return result;
        }

        private static void handleException(Exception ex)
        {
            Log.Fatal(ex);
            if (Throw)
            {
                throw new IOException("An error occured in JsonIO.", ex);
            }
        }

        // -- properties

        /// <summary>
        /// Gets or sets the behavoir, when an internal Exception occures.
        /// </summary>
        public static bool Throw { get; set; } = false;
    }
}
