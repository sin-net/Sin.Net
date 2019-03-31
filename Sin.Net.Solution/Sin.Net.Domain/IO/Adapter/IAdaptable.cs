using System;
using System.Collections.Generic;
using System.Text;

namespace Sin.Net.Domain.IO.Adapter
{
    /// <summary>
    /// This interface specifies how to deserialze a generic data object, mostly a string, into a domain or application specific type.
    /// </summary>
    public interface IAdaptable
    {
        /// <summary>
        /// Converts the input object into the target type Tout
        /// </summary>
        /// <typeparam name="Tin">The generic type parameter for the input</typeparam>
        /// <typeparam name="Tout">The generic type parameter for the output</typeparam>
        /// <param name="input">the input data</param>
        /// <returns>the output data of type Tout</returns>
        Tout Adapt<Tin, Tout>(Tin input) where Tout : new();
    }
}
