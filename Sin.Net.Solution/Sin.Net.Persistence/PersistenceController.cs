using Sin.Net.Domain.IO;
using Sin.Net.Domain.Logging;
using Sin.Net.Persistence.Exports;
using Sin.Net.Persistence.Imports;
using System.Collections.Generic;

namespace Sin.Net.Persistence
{
    // TODO: write unit tests for this

    public class PersistenceController : IPersistenceControlable
    {
        // -- fields

        protected Dictionary<string, IExportable> _exports;
        protected Dictionary<string, IImportable> _imports;

        // -- constructors

        public PersistenceController()
        {
            InitExports();
            InitImports();
        }

        // -- methods

        public virtual void InitExports()
        {
            _exports = new Dictionary<string, IExportable>();
            IExportable export;

            export = new JsonExporter();
            _exports.Add(export.Type, export);

            export = new BinaryExporter();
            _exports.Add(export.Type, export);

            export = new CsvExporter();
            _exports.Add(export.Type, export);

            // add new import types here
        }

        public virtual void InitImports()
        {
            _imports = new Dictionary<string, IImportable>();
            IImportable import;

            import = new JsonImporter();
            _imports.Add(import.Type, import);

            import = new BinaryImporter();
            _imports.Add(import.Type, import);

            import = new CsvImporter();
            _imports.Add(import.Type, import);

            // add new import types here
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
