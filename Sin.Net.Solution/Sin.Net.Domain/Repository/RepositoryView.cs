using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Sin.Net.Domain.Repository
{
    public class RepositoryView<T>
    {

        public RepositoryView()
        {
            Properties = new Dictionary<string, object>();
            Items = new List<T>();
        }

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

        public Dictionary<string, object> Properties { get; set; }

        public List<T> Items { get; set; }
    }
}
