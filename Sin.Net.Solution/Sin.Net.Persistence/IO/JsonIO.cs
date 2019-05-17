using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Sin.Net.Domain.Logging;
using System;
using System.IO;

namespace Sin.Net.Persistence.IO
{

    /// <summary>
    /// Static class to convert between objects and json-strings. 
    /// </summary>
    public static class JsonIO
    {
        #region ToJsonString (2)
        /// <summary>
        /// Creates a json-string out of an object.
        /// </summary>
        /// <param name="obj">The objekt to be serialized.</param>
        /// <returns>The resulting string</returns>
        public static string ToJsonString(object obj)
        {
            return ToJsonString(obj);
        }

        /// <summary>
        /// Creates a json-string out of an object with a custom binder, usefull for interface serializations.
        /// </summary>
        /// <param name="obj">The objekt to be serialized.</param>
        /// <param name="binder">The optional implementation of an ISerializationBinder, e.g. TypedSerializationBinder</param>
        /// <returns>The resulting string<</returns>
        public static string ToJsonString(object obj, ISerializationBinder binder = null)
        {
            string json = "{ }";
            try
            {
                if (binder != null)
                {
                    var settings = new JsonSerializerSettings
                    {
                        TypeNameHandling = TypeNameHandling.Auto,
                        SerializationBinder = binder
                    };
                    json = JsonConvert.SerializeObject(obj, settings);
                }
                else
                {
                    json = JsonConvert.SerializeObject(obj);
                }

            }
            catch (Exception ex)
            {
                json = "{method: 'ObjectIO.ToJsonString()', error: '" + ex.Message + "'}";
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
        /// <returns>The deserialized object of type t</returns>
        public static T FromJsonString<T>(string json, ISerializationBinder binder = null)
        {
            if (binder != null)
            {
                var settings = new JsonSerializerSettings
                {
                    TypeNameHandling = TypeNameHandling.Auto,
                    SerializationBinder = binder
                };
                return JsonConvert.DeserializeObject<T>(json, settings);
            }
            else
            {
                return JsonConvert.DeserializeObject<T>(json);
            }
           
        }

        /// <summary>
        /// Reads a json file and returns the converted object.
        /// </summary>
        /// <typeparam name="T">The typeparam</typeparam>
        /// <param name="file">The complete file path.</param>
        /// <param name="binder">The optional binder implementation.</param>
        /// <returns>The deserialized object of type t.</returns>
        public static T ReadJson<T>(string file, ISerializationBinder binder = null)
        {
            T obj;
            using (StreamReader r = new StreamReader(file))
            {
                string json = r.ReadToEnd();
                obj = FromJsonString<T>(json, binder);
            }

            return obj;
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
        /// Writes an serialized object into a file.
        /// </summary>
        /// <param name="obj">The object to be serialized.</param>
        /// <param name="file">The complete file path.</param>
        /// <param name="binder">The optional binder implementation.</param>
        /// <returns>If successful it returns 'true' otherwise 'false'.</returns>
        public static bool SaveToJson(object obj, string file, ISerializationBinder binder = null)
        {
            bool result = false;
            try
            {
                File.WriteAllText(file, ToJsonString(obj, binder));
                result = true;
            }
            catch (Exception ex)
            {
                Log.Fatal(ex);
            }
            return result;
        }

    }
}
