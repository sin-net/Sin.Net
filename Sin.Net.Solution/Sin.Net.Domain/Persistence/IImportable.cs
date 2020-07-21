using System;
using Sin.Net.Domain.Persistence.Adapter;
using Sin.Net.Domain.Persistence.Settings;

namespace Sin.Net.Domain.Persistence
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
        /// Returns the imported data with direct type conversion.
        /// </summary>
        /// <typeparam name="T">The target type T for the imported data</typeparam>
        /// <returns>The imported data as target type</returns>
        T As<T>() where T : new();

        /// <summary>
        /// Returns the imported data with a manual adoption into the target type.
        /// </summary>
        /// <typeparam name="T">The target type T for the imported data</typeparam>
        /// <param name="casting">The concrete casting functionality</param>
        /// <returns>The imported data as target type</returns>
        T As<T>(IAdaptable adapter) where T : new();

        /// <summary>
        /// Returns the imported data without any type converion.
        /// </summary>
        /// <returns>The imported data as it is.</returns>
        object AsItIs();

        // -- properties

        /// <summary>
        /// Gets or sets the exception that maybe occured during the export pocess.
        /// </summary>
        Exception Exception { get; set; }

        /// <summary>
        /// Gets the import type
        /// </summary>
        string Type { get; }
    }
}
