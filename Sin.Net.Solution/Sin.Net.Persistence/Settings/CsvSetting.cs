using System.Text;

namespace Sin.Net.Persistence.Settings
{
    // TODO: english comments!

    public class CsvSetting : FileSetting
    {
        // -- fields

        private bool _hasHeader;

        // -- constructor

        public CsvSetting()
        {
            Separator = ';';
            HasHeader = true;
        }

        // -- methods

        /// <summary>
        /// Fügt dem aktuellen CustomHeader eine neue Zeile hinzu oder legt diesen an.
        /// </summary>
        /// <param name="line">die zu ergänzende Zeile</param>
        public void AppendHeader(string line = null)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(CustomHeader);

            if (line == null) sb.AppendLine();
            else sb.AppendLine(line);

            CustomHeader = sb.ToString();
        }

        // -- properties

        /// <summary>
        /// Gibt das Trennzeichen zwischen den Spalten aus oder legt dieses fest.
        /// </summary>
        public char Separator { get; set; }

        /// <summary>
        /// Gibt an oder legt fest, ob die erste Zeile als Tabellenkopf ausgewertet werden soll bzw. ob es nutzerspezifische Daten vor den eigentlichen Csv-Daten gibt (true),
        /// Wenn die erste Zeile bereits Csv-Daten enthält und die Eigenschaft CustomHeader null ist, wird 'false' ausgegeben.
        /// </summary>
        public bool HasHeader
        {
            get
            {
                return (_hasHeader == true || !string.IsNullOrEmpty(CustomHeader));
            }
            set
            {
                _hasHeader = value;
            }
        }

        /// <summary>
        /// Gibt an oder legt fest welcher Text vor den eigentlichen Csv-Daten in der Ausgabedatei stehen sollen
        /// </summary>
        public string CustomHeader { get; set; }
    }
}
