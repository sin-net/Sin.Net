using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Sin.Net.Domain.Repository
{
    /// <summary>
    /// The purpose of this class is to convert repository-based class into a representation that can be serialized into json data.
    /// This class type can be used for serialization or for views in a web-based application.
    /// </summary>
    /// <typeparam name="T">The type of the items that are stored in the target repository</typeparam>
    public class RepositoryView<T>
    {
        // -- constructor

        public RepositoryView()
        {
            Properties = new Dictionary<string, object>();
            Items = new List<T>();
        }

        // -- methods

        /// <summary>
        /// Creates a representation of the repository for json serialization or web-views.
        /// The custom properties of the properties will be stored in a dictionary called Properties and
        /// the Items list will be copied.
        /// </summary>
        /// <param name="repository">The instance of a concretion of the repository base class with custom properties and a list of items.</param>
        /// <returns>The calling instance with the copied fields.</returns>
        public RepositoryView<T> From(RepositoryBase<T> repository)
        {
            // items
            Items = repository.Items;

            // properties
            var infos = GetInfo(repository);
            foreach (PropertyInfo pi in infos)
            {
                Properties.Add(pi.Name, pi.GetValue(repository, null));
            }

            return this;
        }

        /// <summary>
        /// Transforms the calling instance into a regular repository class that inherits from RepositoryBase.
        /// </summary>
        /// <typeparam name="Tout">the concrete type, that the calling object will be converted into.</typeparam>
        /// <returns>The repository isntance where all mathing properties and the Items list are copied.</returns>
        public Tout To<Tout>() where Tout : RepositoryBase<T>, new()
        {
            // items
            Tout repository = new Tout();
            repository.AddRange(this.Items.ToArray());

            // properties
            var infos = GetInfo(repository);
            foreach (PropertyInfo pi in infos)
            {
                if (pi.CanWrite &&
                    Properties.ContainsKey(pi.Name))
                {
                    var raw = Properties[pi.Name];
                    var data = Convert.ChangeType(raw, pi.PropertyType);
                    pi.SetValue(repository, data);
                }
            }

            return repository;
        }

        private PropertyInfo[] GetInfo(RepositoryBase<T> repository)
        {
            // remove the property Items or items because this is the list in the RepositoryBase class
            return repository
                .GetType()
                .GetProperties()
                .Where(p => p.Name.ToLower().Equals("items") == false)
                .ToArray();
        }

        // -- properties

        /// <summary>
        /// Gets or sets all custom properties.
        /// </summary>
        public Dictionary<string, object> Properties { get; set; }
    
        /// <summary>
        /// Gets or sets all items in a list.
        /// </summary>
        public List<T> Items { get; set; }
    }
}
