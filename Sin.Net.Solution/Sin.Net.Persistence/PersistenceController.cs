using Sin.Net.Domain.Persistence;
using Sin.Net.Domain.Persistence.Logging;
using Sin.Net.Persistence.Exports;
using Sin.Net.Persistence.Imports;
using System.Collections.Generic;

namespace Sin.Net.Persistence
{
    public class PersistenceController : IPersistenceControlable
    {
        // -- fields

        protected Dictionary<string, IExportable> _exports;
        protected Dictionary<string, IImportable> _imports;

        // -- constructors

        public PersistenceController(bool init = true)
        {
            if (init)
            {
                InitExports();
                InitImports();
            }
        }

        // -- methods

        public virtual IPersistenceControlable InitExports()
        {
            _exports = new Dictionary<string, IExportable>();
            IExportable export;

            export = new JsonExporter();
            _exports.Add(export.Type, export);

            export = new BinaryExporter();
            _exports.Add(export.Type, export);

            export = new CsvExporter();
            _exports.Add(export.Type, export);

            // add new default export types here
            return this;
        }

        public virtual IPersistenceControlable InitImports()
        {
            _imports = new Dictionary<string, IImportable>();
            IImportable import;

            import = new JsonImporter();
            _imports.Add(import.Type, import);

            import = new BinaryImporter();
            _imports.Add(import.Type, import);

            import = new CsvImporter();
            _imports.Add(import.Type, import);

            // add new default import types here
            return this;
        }

        /// <summary>
        /// Adds a new importer and uses the name of the type as key.
        /// </summary>
        /// <param name="importer"></param>
        /// <returns></returns>
        public IPersistenceControlable Add(IImportable importer)
        {
            Add(importer.GetType().Name, importer);
            return this;
        }

        /// <summary>
        /// Adds a new importer and uses the specific key.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="importer"></param>
        /// <returns></returns>
        public IPersistenceControlable Add(string key, IImportable importer)
        {
            _imports.Add(key, importer);
            return this;
        }

        /// <summary>
        /// Adds a new exporter and uses the name of the type as key.
        /// </summary>
        /// <param name="exporter"></param>
        /// <returns></returns>
        public IPersistenceControlable Add(IExportable exporter)
        {
            Add(exporter.GetType().Name, exporter);
            return this;
        }

        /// <summary>
        /// Adds a new exporter and uses the specific key.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="exporter"></param>
        /// <returns></returns>
        public IPersistenceControlable Add(string key, IExportable exporter)
        {
            _exports.Add(key, exporter);
            return this;
        }

        public IExportable Exporter(string key)
        {
            if (_exports.ContainsKey(key) == false)
            {
                ErrorWith<IExportable>(key);
                return null;
            }
            return _exports[key];
        }

        public IImportable Importer(string key)
        {
            if (_imports.ContainsKey(key) == false)
            {
                ErrorWith<IImportable>(key);
                return null;
            }
            return _imports[key];
        }

        /// <summary>
        /// Writes the log error if an ex- or import inxtance was not found.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        private void ErrorWith<T>(string key)
        {
            Log.Error($"The '{nameof(T)}' with the key '{key}' was not found.", this);
        }
    }
}
