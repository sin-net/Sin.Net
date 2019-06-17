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
            DataPosition = 1;
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
        /// Gets or sets wheather the column names are used as header line or not.
        /// </summary>
        public bool HasHeader { get; set; }

        /// <summary>
        /// Gets or sets addional header line before the csv-data.
        /// </summary>
        public string CustomHeader { get; set; }

    
        /// <summary>
        /// Gets or sets the line number of the first data row.
        /// </summary>
        public int DataPosition { get; set; }
    }
}
