﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Sin.Net.Domain.Repository
{
    [Serializable]
    public abstract class RepositoryBase<T>
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

        // -- properties

        /// <summary>
        /// Gets or sets the name of the repository
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets the items of the repository and allows it for derived classes to set the items list
        /// </summary>
        public List<T> Items { get; set; }

        /// <summary>
        /// Gets the count of the items list as a shortcut property 
        /// </summary>
        public int Count => Items.Count;

        // -- indexer

        /// <summary>
        /// index-based intexer to be able to get the instance at the index i
        /// </summary>
        /// <param name="index">The index of the item in the list</param>
        /// <returns>The instance of T with the corresponding index</returns>
        public T this[int index] => Items[index];
    }
}
