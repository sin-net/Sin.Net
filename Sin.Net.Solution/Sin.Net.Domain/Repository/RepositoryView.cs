using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using System.Reflection;

namespace Sin.Net.Domain.Repository
{
    public class RepositoryView<T> : Dictionary<string, object>
    {

        public RepositoryView()
        {
        }

        public RepositoryView<T> From(RepositoryBase<T> repository)
        {
            // list items
            Items = repository.Items;

            // properties

            PropertyInfo[] properties = repository.GetType().GetProperties();
            foreach (PropertyInfo pi in properties)
            {
                this.Add(pi.Name, pi.GetValue(repository, null));
            }

            return this;
        }

        // -- properties

        public List<T> Items { get; set; }
    }
}
