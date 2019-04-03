namespace Sin.Net.Domain.IO
{
    /// <summary>
    /// Interface for controlling IO functionality based on strategy-pattern to access import and export conretions
    /// </summary>
    public interface IPersistenceControlable
    {
        /// <summary>
        /// Initalizes all export implementations
        /// </summary>
        void InitExports();

        /// <summary>
        /// Initializes all import implementations 
        /// </summary>
        void InitImports();

        /// <summary>
        /// Gets the concrete export functionality based on its key as identifier
        /// </summary>
        /// <param name="key">The string to access the export instance</param>
        /// <returns>The export instance</returns>
        IExportable Exporter(string key);

        /// <summary>
        /// Gets the concrete import functionality based on its key as identifier
        /// </summary>
        /// <param name="key">The key to access the import instance</param>
        /// <returns>The import instance</returns>
        IImportable Importer(string key);
    }
}
