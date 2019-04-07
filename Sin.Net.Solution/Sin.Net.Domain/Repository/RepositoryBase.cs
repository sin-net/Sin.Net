using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sin.Net.Domain.Repository
{
    public class RepositoryBase<T>
    {
        // -- constructor

        /// <summary>
        /// Base constructor that instanciates the items list.
        /// </summary>
        public RepositoryBase()
        {
            Items = new List<T>();
        }

        // -- methods

        //public T FirstOrNew(Func<T, bool> predicate, out bool created)
        //{
        //    var item = Items.FirstOrDefault(predicate);

        //    if (item != null)
        //    {
        //        created = false;
        //        return item;
        //    }
        //    else
        //    {
        //        created = true;
        //        Items.Add(new T());
        //        return Items.Last();
        //    }
        //}

        /// <summary>
        /// overwritten method
        /// </summary>
        /// <returns>Returns the name property and the count of items</returns>
        public override string ToString()
        {
            return $"{Name}: {Items.Count} {(Items.Count == 1 ? "item" : "items")}";
        }

        // -- properties

        /// <summary>
        /// Gets or sets the name of the repository
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets the items of the repository and allows it for derived classes to set the items list
        /// </summary>
        public List<T> Items { get; protected set; }

        /// <summary>
        /// Gets the count of the items list as a shortcut property 
        /// </summary>
        public int Count => Items.Count;
    }
}
