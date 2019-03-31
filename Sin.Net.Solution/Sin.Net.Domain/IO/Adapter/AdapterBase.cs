using Sin.Net.Domain.Logging;
using System;

namespace Sin.Net.Domain.IO.Adapter
{
    // TODO make english comments
    public abstract class AdapterBase : IAdaptable
    {
        /// <summary>
        /// Abstrakte Methode des IAdaptable Schnittstelle
        /// Die Ein und Ausgabetypen werden hier deklariert, damit jeder Im- Exporter mit beliebigen Adaptern ohne Typbeschränkung arbeiten kann
        /// </summary>
        /// <typeparam name="Tin">der generische Typparameter für die Eingangsdaten</typeparam>
        /// <typeparam name="Tout">der generische Typparameter für die Ausgangsdaten</typeparam>
        /// <param name="input">Die Eingagnsdaten</param>
        /// <returns>Die Ausgangsdaten</returns>
        public abstract Tout Adapt<Tin, Tout>(Tin input) where Tout : new();

        /// <summary>
        /// Validiert die generischen Ein- und Ausgabetypen, die der Adapter verarbeiten soll.
        /// </summary>
        /// <typeparam name="Tin">Eingabetyp der Adapt Methode</typeparam>
        /// <typeparam name="Tout">Ausgabetyp der Adapt Methode</typeparam>
        /// <param name="inType">Type-Objekt der Eingabedaten</param>
        /// <param name="outType">Type-Objekt der Ausgabedaten</param>
        /// <returns>Wenn die generischen Typparameter und die Typ-Objekte übereinstimmen wird true, andernfalls false ausgegeben.</returns>
        public bool ValidateTypes<Tin, Tout>(Type inType, Type outType)
        {
            if (typeof(Tin).IsAssignableFrom(inType) == false ||
                typeof(Tout).IsAssignableFrom(outType) == false)
            {
                Log.Error($"The adaption from '{inType.Name}' to '{outType.Name}' failed.", this);
                return false;
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// Konvertiert das Ausgabeobjekt des Adapterobjektes in den generischen Ausgabetyp.
        /// </summary>
        /// <typeparam name="T">Der Ausgabetyp</typeparam>
        /// <param name="data">Das Datenobjekt</param>
        /// <returns>der konvertierte generische Ausgabetyp</returns>
        public T ConvertOutput<T>(object data)
        {
            return (T)Convert.ChangeType(data, typeof(T));
        }

    }
}
