// <copyright file="INode.cs" company="Jeremy Regnerus">
// Copyright (c) Jeremy Regnerus. All rights reserved.
// </copyright>

using System.Collections.Generic;

namespace JSR.BaseClassLibrary
{
    /// <summary>
    /// Interface for Node / Tree structure.
    /// </summary>
    /// <typeparam name="T">Type of object contained within the Node.</typeparam>
    public interface INode<T>
    {
        /// <summary>
        /// Gets a value indicating whether this is a root Node.
        /// </summary>
        bool IsRoot { get; }

        /// <summary>
        /// Gets or sets the Item managed by this Node.
        /// </summary>
        T Item { get; set; }

        /// <summary>
        /// Gets a list of child Nodes within this Node.
        /// </summary>
        BaseCollection<INode<T>> Children { get; }

        /// <summary>
        /// Gets or sets the Parent to this Node.
        /// </summary>
        INode<T> Parent { get; set; }

        /// <summary>
        /// Gets a value indicating whether this Node has Child components.
        /// </summary>
        bool HasChildren { get; }

        /// <summary>
        /// Gets the Root node for the tree that this Node is contained within.
        /// </summary>
        INode<T> Root { get; }

        /// <summary>
        /// Adds a new child Node with an Item to the Children collection.
        /// </summary>
        /// <param name="item">The Item object within the new Child Node.</param>
        /// <param name="unique">Only add items if they do not exist in the list already.</param>
        void AddChild(T item, bool unique);

        /// <summary>
        /// Add a Child Node to the Children collection.
        /// </summary>
        /// <param name="node">Child Node to add to the Children collection.</param>
        /// <param name="unique">Only add if the item does not exist in the list already.</param>
        void AddChild(INode<T> node, bool unique);

        /// <summary>
        /// Add multiple items to the Children collection.
        /// </summary>
        /// <param name="items">Items to add.</param>
        /// <param name="unique">Only add items if they do not exist in the list already.</param>
        void AddRange(IEnumerable<T> items, bool unique);

        /// <summary>
        /// Add multiple nodes to the Children collection.
        /// </summary>
        /// <param name="nodes">Nodes to add.</param>
        /// <param name="unique">Only add items if they do not exist in the list already.</param>
        void AddRange(IEnumerable<INode<T>> nodes, bool unique);

        /// <summary>
        /// Get the number of children.
        /// </summary>
        /// <param name="recursive">Include all ancestors.</param>
        /// <param name="unique">Only get the number of unique Items.</param>
        /// <param name="fromRoot">Get the value from the Root Node.</param>
        /// <returns>Number of children.</returns>
        int GetCount(bool recursive, bool unique, bool fromRoot = false);

        /// <summary>
        /// Gets a new copy of this node.
        /// </summary>
        /// <param name="parent">The new parent to assign to the copy.</param>
        /// <param name="withChildren">Whether the node should come with copies of its children.</param>
        /// <returns>A copy of this node.</returns>
        INode<T> GetCopy(INode<T> parent, bool withChildren = true);

        /// <summary>
        /// Get a list of the Items in this Node.
        /// </summary>
        /// <param name="recursive">Get all ancestor items.</param>
        /// <param name="unique">Only get one instance of each item.</param>
        /// <param name="fromRoot">Get items from the Root of this Node's tree.</param>
        /// <returns>A list of Items in the Node.</returns>
        List<T> GetItems(bool recursive, bool unique, bool fromRoot = false);

        /// <summary>
        /// Checks if the items already exists within the tree.
        /// </summary>
        /// <param name="item">Item to evaluate.</param>
        /// <param name="recursive">Check all ancestors.</param>
        /// <param name="fromRoot">Start from the Root Node.</param>
        /// <returns>True if the item exists.</returns>
        bool ItemExists(T item, bool recursive, bool fromRoot = false);
    }
}
