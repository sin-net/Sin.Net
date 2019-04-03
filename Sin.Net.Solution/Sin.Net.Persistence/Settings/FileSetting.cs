using Sin.Net.Domain.IO.Settings;
using System;
using System.IO;
using System.Linq;

namespace Sin.Net.Persistence.Settings
{
    // TODO: english comments

    /// <summary>
    /// Die Klasse repräsentiert alle Einstellungsmöglichkeiten zum Lesen oder Schreiben Dateien
    /// </summary>
    public class FileSetting : SettingsBase
    {
        // -- constructor

        public FileSetting()
        {
            Location = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "App_Data");
        }

        // -- properties

        /// <summary>
        /// Gibt den Pfad ohne die Dateiendung aus oder legt diese fest
        /// </summary>
        public string Location { get; set; }

        /// <summary>
        /// Gibt die Dateiendung aus
        /// </summary>
        public string Extension
        {
            get
            {
                string ext = string.Empty;
                if (Name.Contains("."))
                {
                    ext = Name.Split('.').Last();
                }

                return ext;
            }
        }

        /// <summary>
        /// Gibt den vollständigen Pfad mit Dateinamen aus.
        /// </summary>
        public string FullPath => Path.Combine(Location, base.Name);

    }
}
