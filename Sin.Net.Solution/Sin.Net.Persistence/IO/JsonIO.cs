using Newtonsoft.Json;
using Sin.Net.Domain.Logging;
using System;
using System.IO;

namespace Sin.Net.Persistence.IO
{
    // TODO: make english comments

    /// <summary>
    /// Statische Klasse zur Konvertierung von Objekten und serielle Binär- bzw JSON-Daten 
    /// </summary>
    internal static class JsonIO
    {
        #region ToJsonString (2)
        /// <summary>
        /// Erstellt eine Zeichenkette im json-Format aus einem beliebigen Objekt
        /// </summary>
        /// <param name="obj">Das Objekt mit beliebig kaskadierten Eigenschaften</param>
        /// <returns>die resuliterende Zeichenkette</returns>
        public static string ToJsonString(object obj)
        {
            return ToJsonString(obj, Formatting.None);
        }
        /// <summary>
        /// Erstellt eine Zeichenkette im json-Format aus einem beliebigen Objekt
        /// </summary>
        /// <param name="obj">Das Objekt mit beliebig kaskadierten Eigenschaften</param>
        /// <param name="format">Das gewünschte Format, mit Zeilenumbrüchen, oder ohne.</param>
        /// <returns>die resuliterende Zeichenkette</returns>
        public static string ToJsonString(object obj, Formatting format)
        {
            string json = "{ }";
            try
            {
                json = JsonConvert.SerializeObject(obj, format);
            }
            catch (Exception ex)
            {
                json = "{method: 'ObjectIO.ToJsonString()', error: '" + ex.Message + "'}";
            }
            return json;
        }
        #endregion

        /// <summary>
        /// Konvertiert einen string in ein Objekt vom Typ T
        /// </summary>
        /// <typeparam name="T">Das Objekttyp</typeparam>
        /// <param name="json">der json string</param>
        /// <returns></returns>
        public static T FromJsonString<T>(string json)
        {
            return JsonConvert.DeserializeObject<T>(json);
        }

        /// <summary>
        /// Lädt aus der angegebenen Datei das generische Objekt T und gibt dieses aus.
        /// </summary>
        /// <typeparam name="T">Das Objekttyp</typeparam>
        /// <param name="file">Die Zieldatei, inkl. Pfadangabe</param>
        /// <returns>Das ausgelesene Objekt vom Typ T.</returns>
        public static T ReadJson<T>(string file)
        {
            T obj;
            using (StreamReader r = new StreamReader(file))
            {
                string json = r.ReadToEnd();
                obj = FromJsonString<T>(json);
            }

            return obj;
        }

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
        /// Schreibt die Daten eines beliebigen Objektes in die angegebene Datei.
        /// </summary>
        /// <param name="obj">Das Objekt, welches eingelesen wird</param>
        /// <param name="file">Die Zieldatei, inkl. Pfadangabe</param>
        public static bool SaveToJson(object obj, string file)
        {
            bool result = false;
            try
            {
                File.WriteAllText(file, ToJsonString(obj, Formatting.Indented));
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
