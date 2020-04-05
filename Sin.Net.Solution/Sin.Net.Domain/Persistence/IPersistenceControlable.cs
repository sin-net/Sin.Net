namespace Sin.Net.Domain.Persistence
{
    /// <summary>
    /// Interface for controlling IO functionality based on strategy-pattern to access import and export conretions
    /// </summary>
    public interface IPersistenceControlable
    {
        /// <summary>
        /// Initalizes all export implementations
        /// </summary>
        IPersistenceControlable InitExports();

        /// <summary>
        /// Initializes all import implementations 
        /// </summary>
        IPersistenceControlable InitImports();

        IPersistenceControlable Add(IImportable importer);

        IPersistenceControlable Add(string key, IImportable importer);
        
        IPersistenceControlable Add(IExportable exporter);

        IPersistenceControlable Add(string key, IExportable exporter);

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
