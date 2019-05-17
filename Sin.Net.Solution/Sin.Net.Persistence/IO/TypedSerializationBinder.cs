using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sin.Net.Persistence.IO
{
    public class TypedSerializationBinder : ISerializationBinder
    {
        // -- constructor

        /// <summary>
        /// Default constructor with an empty list of known types.
        /// </summary>
        public TypedSerializationBinder()
        {
            KnownTypes = new List<Type>();
        }

        /// <summary>
        /// Constructor with a list of known types as parameter.
        /// </summary>
        /// <param name="types">The list of predéfined types.</param>
        public TypedSerializationBinder(List<Type> types)
        {
            KnownTypes = types;
        }

        // -- methods

        /// <summary>
        /// Overriden Method to bind an object to an type.
        /// </summary>
        /// <param name="assemblyName"></param>
        /// <param name="typeName"></param>
        /// <returns></returns>
        public Type BindToType(string assemblyName, string typeName)
        {
            return KnownTypes.SingleOrDefault(t => t.Name == typeName);
        }

        /// <summary>
        /// Overridden Method to bind a type to a name.
        /// </summary>
        /// <param name="serializedType"></param>
        /// <param name="assemblyName"></param>
        /// <param name="typeName"></param>
        public void BindToName(Type serializedType, out string assemblyName, out string typeName)
        {
            assemblyName = null;
            typeName = serializedType.Name;
        }

        // -- properties

        /// <summary>
        /// Gets or sets the List of types that should be resolved
        /// </summary>
        public List<Type> KnownTypes { get; set; }
    }
}
