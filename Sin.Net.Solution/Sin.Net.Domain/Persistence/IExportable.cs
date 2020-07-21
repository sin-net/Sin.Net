using System;
using Sin.Net.Domain.Persistence.Adapter;
using Sin.Net.Domain.Persistence.Settings;

namespace Sin.Net.Domain.Persistence
{
    /// <summary>
    /// Interface for concrete export functionality
    /// </summary>
    public interface IExportable
    {
        /// <summary>
        /// Takes the cocnrete implementation of settings
        /// </summary>
        /// <param name="setting">settings concretion</param>
        /// <returns>the calling IExportable-instance for method chaining</returns>
        IExportable Setup(SettingsBase setting);

        /// <summary>
        /// Takes a generic data type that shall be exported
        /// </summary>
        /// <typeparam name="T">the explicit type of the data</typeparam>
        /// <param name="data">the instance of the data</param>
        /// <returns>the calling IExportable-instance for method chaining</returns>
        IExportable Build<T>(T data);

        /// <summary>
        ///  Takes a generic data type that shall be exported
        /// </summary>
        /// <typeparam name="T">the explicit type of the data</typeparam>
        /// <param name="data">the instance of the data</param>
        /// <param name="adapter">The concretion of an adapter that converts the data before the export</param>
        /// <returns>the calling IExportable-instance for method chaining</returns>
        IExportable Build<T>(T data, IAdaptable adapter);

        /// <summary>
        /// Runs the export implementation
        /// </summary>
        /// <returns>The string is a generic result format and is specified by the export concretion</returns>
        string Export();

        // -- properties

        /// <summary>
        /// Gets or sets the exception that maybe occured during the export pocess.
        /// </summary>
        Exception Exception { get; set; }

        /// <summary>
        /// Gets the export type
        /// </summary>
        string Type { get; }
    }
}
