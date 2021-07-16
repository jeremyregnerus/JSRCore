// <copyright file="Node.cs" company="Jeremy Regnerus">
// Copyright (c) Jeremy Regnerus. All rights reserved.
// </copyright>

using System.Collections.Generic;

namespace JSR.BaseClassLibrary
{
    /// <summary>
    /// Node / Tree structure for managing objects.
    /// </summary>
    /// <typeparam name="T">Type of object contained in this Node within the Item property.</typeparam>
    public class Node<T> : BaseClass
    {
        private T item;
        private Node<T> parent;

        /// <summary>
        /// Initializes a new instance of the <see cref="Node{T}"/> class.
        /// </summary>
        /// <param name="item">Item contained within this node.</param>
        public Node(T item)
        {
            Item = item;
        }

        /// <summary>
        /// Gets a value indicating whether this is a root Node.
        /// </summary>
        public bool IsRoot { get => Parent == null; }

        /// <summary>
        /// Gets or sets the Item managed by this Node.
        /// </summary>
        public T Item { get => item; set => SetValue(value, ref item); }

        /// <summary>
        /// Gets a list of child Nodes within this Node.
        /// </summary>
        public BaseCollection<Node<T>> Children { get; } = new BaseCollection<Node<T>>();

        /// <summary>
        /// Gets or sets the Parent to this Node.
        /// </summary>
        public Node<T> Parent { get => parent; set => SetValue(value, ref parent); }

        /// <summary>
        /// Gets a value indicating whether this Node has Child components.
        /// </summary>
        public bool HasChildren { get => Children.Count > 0; }

        /// <summary>
        /// Gets the Root node for the tree that this Node is contained within.
        /// </summary>
        public Node<T> Root
        {
            get
            {
                if (parent == null)
                {
                    return this;
                }

                return parent.Root;
            }
        }

        /// <summary>
        /// Adds a new child Node with an Item to the Children collection.
        /// </summary>
        /// <param name="item">The Item object within the new Child Node.</param>
        public void AddChild(T item)
        {
            Children.Add(new Node<T>(item) { Parent = this });
        }

        /// <summary>
        /// Add a Child Node to the Children collection.
        /// </summary>
        /// <param name="node">Child Node to add to the Children collection.</param>
        public void AddChild(Node<T> node)
        {
            node.Parent = this;
            Children.Add(node);
        }

        /// <summary>
        /// Add multiple items to the Children collection.
        /// </summary>
        /// <param name="items">Items to add.</param>
        public void AddRange(IEnumerable<T> items)
        {
            foreach (T item in items)
            {
                AddChild(item);
            }
        }

        /// <summary>
        /// Add multiple nodes to the Children collection.
        /// </summary>
        /// <param name="nodes">Nodes to add.</param>
        public void AddRance(IEnumerable<Node<T>> nodes)
        {
            foreach (Node<T> node in nodes)
            {
                AddChild(node);
            }
        }

        /// <summary>
        /// Get the number of children.
        /// </summary>
        /// <param name="recursive">Include all ancestors.</param>
        /// <param name="unique">Only get the number of unique Items.</param>
        /// <param name="fromRoot">Get the value from the Root Node.</param>
        /// <returns>Number of children.</returns>
        public int GetCount(bool recursive, bool unique, bool fromRoot = false)
        {
            if (fromRoot)
            {
                return Root.GetCount(recursive, unique);
            }

            if (!unique)
            {
                int i = Children.Count;

                if (recursive)
                {
                    foreach (Node<T> child in Children)
                    {
                        i += child.GetCount(recursive, unique);
                    }
                }

                return i;
            }

            return GetChildItems(recursive, unique).Count;
        }

        /// <summary>
        /// Get a list of the Items in this Node.
        /// </summary>
        /// <param name="recursive">Get all ancestor items.</param>
        /// <param name="unique">Only get one instance of each item.</param>
        /// <param name="fromRoot">Get items from the Root of this Node's tree.</param>
        /// <returns>A list of Items in the Node.</returns>
        public List<T> GetChildItems(bool recursive, bool unique, bool fromRoot = false)
        {
            if (fromRoot)
            {
                return Root.GetChildItems(recursive, unique);
            }

            List<T> items = new List<T>();

            foreach (Node<T> child in Children)
            {
                if (!unique)
                {
                    items.Add(child.Item);

                    if (recursive)
                    {
                        items.AddRange(child.GetChildItems(recursive, unique));
                    }

                    continue;
                }

                if (!items.Contains(child.Item))
                {
                    items.Add(child.Item);

                    if (recursive)
                    {
                        foreach (T item in child.GetChildItems(recursive, unique))
                        {
                            if (!items.Contains(item))
                            {
                                items.Add(item);
                            }
                        }
                    }
                }
            }

            return items;
        }
    }
}
