using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Sin.Net.Domain.Persistence.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

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
        /// <returns>The resulting string<</returns>
        public static string ToJsonString(object obj)
        {
            string json = "{ }";
            try
            {
                var settings = new JsonSerializerSettings
                {
                    TypeNameHandling = TypeNameHandling.Auto,
                    SerializationBinder = Binder,
                    Converters = Converters,
                    ContractResolver = Resolver
                };

                if (EnableCaseResolver)
                {
                    settings.ContractResolver = new LowerCaseContractResolver();
                }

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
        /// <returns>The deserialized object of type t</returns>
        public static T FromJsonString<T>(string json)
        {
            try
            {
                var settings = new JsonSerializerSettings
                {
                    TypeNameHandling = TypeNameHandling.Auto,
                    SerializationBinder = Binder,
                    Converters = Converters,
                    ContractResolver = Resolver
                };

                if (EnableCaseResolver)
                {
                    settings.ContractResolver = new UpperCaseContractResolver();
                }

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
            using (StreamReader r = new StreamReader(file, FileEncoding))
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
        public static T ReadJson<T>(string file)
        {
            T obj;
            using (StreamReader r = new StreamReader(file, FileEncoding))
            {
                string json = r.ReadToEnd();
                obj = FromJsonString<T>(json);
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
        public static bool SaveToJson(object obj, string file)
        {
            bool result = false;
            try
            {
                File.WriteAllText(file, ToJsonString(obj), FileEncoding);
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

        /// <summary>
        /// Gets or sets the feature of resolving property names to lower case at serializing. 
        /// </summary>
        public static bool EnableCaseResolver { get; set; } = false;

        /// <summary>
        /// Gets or sets the encoding of json string for reading and writing files.
        /// </summary>
        public static Encoding FileEncoding { get; set; } = Encoding.Default;

        /// <summary>
        /// Gets or sets the resolver that defines how a specific object should be serialized and vice versa.
        /// </summary>
        public static IContractResolver Resolver { get; set; }

        /// <summary>
        /// Gets or sets the serialization binder.
        /// </summary>
        public static ISerializationBinder Binder { get; set; }

        /// <summary>
        /// Gets or sets the list of converters that define how base classes or interfaces will be converted.
        /// </summary>
        public static List<JsonConverter> Converters { get; set; }
    }
}
