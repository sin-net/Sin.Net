using System.Collections;
using System.Collections.Generic;

namespace Sin.Net.Domain.Repository
{
    public abstract class RepositoryBase<T> : IEnumerable
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

        /// <summary>
        /// Returns the Enumerator of the Items list.
        /// </summary>
        /// <returns></returns>
        public IEnumerator GetEnumerator()
        {
            return Items.GetEnumerator();
        }

        /// <summary>
        /// Takes the searchItem and processes a search in the repository
        /// </summary>
        /// <param name="searchItem">The item for the search</param>
        /// <returns>True if it was found, false if not</returns>
        public virtual bool Contains(T searchItem)
        {
            return Items.Contains(searchItem);
        }

        /// <summary>
        /// Adds a new item to the repository
        /// </summary>
        /// <param name="item">the new instance of T that shall be added to the list</param>
        /// <returns>returns the new item</returns>
        public virtual T Add(T item)
        {
            Items.Add(item);
            return item;
        }

        public virtual void AddRange(T[] items)
        {
            Items.AddRange(items);
        }

        /// <summary>
        /// Removes an item at a specific index
        /// </summary>
        /// <param name="index">zero-based index</param>
        public virtual void RemoveAt(int index)
        {
            Items.RemoveAt(index);
        }

        /// <summary>
        /// Removes an specific item that is stored in the repository
        /// </summary>
        /// <param name="item">the instance of T that shall be removed</param>
        /// <returns></returns>
        public virtual bool Remove(T item)
        {
            return Items.Remove(item);
        }

        public virtual void Clear()
        {
            Items.Clear();
        }

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
