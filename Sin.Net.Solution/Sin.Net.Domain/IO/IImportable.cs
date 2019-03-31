using Sin.Net.Domain.IO.Adapter;
using Sin.Net.Domain.IO.Settings;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sin.Net.Domain.IO
{
    /// <summary>
    /// Interface for concrete import functionality
    /// </summary>
    public interface IImportable
    {
        /// <summary>
        /// Takes the cocnrete implementation of settings
        /// </summary>
        /// <param name="setting">settings concretion</param>
        /// <returns>the calling IImportable-instance for method chaining</returns>
        IImportable Setup(SettingsBase setting);

        /// <summary>
        /// Runs the import implementation
        /// </summary>
        /// <returns>the calling IImportable-instance for method chaining</returns>
        IImportable Import();

        /// <summary>
        /// Returns the imported data without type conversions
        /// </summary>
        /// <typeparam name="T">The target type T for the imported data</typeparam>
        /// <returns>The imported data as target type</returns>
        T Get<T>() where T : new();

        /// <summary>
        /// Casts the imported data instance in the generic result type T 
        /// </summary>
        /// <typeparam name="T">The target type T for the imported data</typeparam>
        /// <param name="casting">The concrete casting functionality</param>
        /// <returns>The imported data as target type</returns>
        T With<T>(IAdaptable adapter) where T : new();

        // -- properties

        /// <summary>
        /// Gets the import type
        /// </summary>
        string Type { get; }
    }
}
