using System;
using System.Collections;
using System.Collections.Generic;

namespace Sin.Net.Domain.Repository
{
    /// <summary>
    /// This abstract base class allows to store a list of T and setup additional properties in its concretions.
    /// It implements the IEnumerable interface so it can directly be iterated in foreach or other loops.
    /// It is not suitable for json serialization because the enumerator causes the serialization to skip other user defines properties 
    /// </summary>
    /// <typeparam name="T">The type strored in the Items property</typeparam>
    [Serializable]
    public abstract class IterativeRepositoryBase<T> : RepositoryBase<T>, IEnumerable
    {
        // -- constructor

        /// <summary>
        /// Base constructor that instanciates the items list.
        /// </summary>
        public IterativeRepositoryBase() : base()
        {
        }

        // -- methods

        /// <summary>
        /// Returns the Enumerator of the Items list.
        /// </summary>
        /// <returns></returns>
        public IEnumerator GetEnumerator()
        {
            return Items.GetEnumerator();
        }
    }
}
