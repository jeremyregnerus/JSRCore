// <copyright file="Node.cs" company="Jeremy Regnerus">
// Copyright (c) Jeremy Regnerus. All rights reserved.
// </copyright>

using System.Collections.Generic;
using System.Linq;

namespace JSR.BaseClassLibrary
{
    /// <summary>
    /// Node / Tree structure for managing objects.
    /// </summary>
    /// <typeparam name="T">Type of object contained in this Node within the Item property.</typeparam>
    public class Node<T> : BaseClass where T : BaseClass
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
        /// Initializes a new instance of the <see cref="Node{T}"/> class.
        /// </summary>
        /// <param name="item">Item contained within this node.</param>
        /// <param name="parent">Parent Node of this Node.</param>
        public Node(T item, Node<T> parent) : this(item)
        {
            Parent = parent;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Node{T}"/> class.
        /// </summary>
        /// <param name="item">Item contained within this node.</param>
        /// <param name="children">Children Nodes of this Node.</param>
        public Node(T item, IEnumerable<Node<T>> children) : this(item)
        {
            Children = new BaseCollection<Node<T>>(children);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Node{T}"/> class.
        /// </summary>
        /// <param name="item">Item contained within this node.</param>
        /// <param name="parent">Parent Node of this Node.</param>
        /// <param name="children">Children Nodes of this Node.</param>
        public Node(T item, Node<T> parent, IEnumerable<Node<T>> children) : this(item, parent)
        {
            Children = new BaseCollection<Node<T>>(children);
        }

        /// <summary>
        /// Gets a list of child Nodes within this Node.
        /// </summary>
        public BaseCollection<Node<T>> Children { get; } = new BaseCollection<Node<T>>();

        /// <summary>
        /// Gets a value indicating whether this is a root Node.
        /// </summary>
        public bool IsRoot { get => Parent == null; }

        /// <summary>
        /// Gets a value indicating whether this is a lead Node.
        /// </summary>
        public bool IsLeaf { get => Children.Count == 0; }

        /// <summary>
        /// Gets or sets the Item managed by this Node.
        /// </summary>
        public T Item { get => item; set => SetValue(value, ref item); }

        /// <summary>
        /// Gets or sets the Parent to this Node.
        /// </summary>
        public Node<T> Parent { get => parent; set => SetValue(value, ref parent); }

        /// <summary>
        /// Gets the Root node for the tree that this Node is contained within.
        /// </summary>
        public Node<T> Root { get => Parent is null ? this : Parent.Root; }

        /// <summary>
        /// Adds a new child Node with an Item to the Children collection.
        /// </summary>
        /// <param name="item">The Item object within the new Child Node.</param>
        /// <param name="unique">Only add items if they do not exist in the list already.</param>
        public void AddChild(T item, bool unique)
        {
            AddChild(new Node<T>(item), unique);
        }

        /// <summary>
        /// Add a Child Node to the Children collection.
        /// </summary>
        /// <param name="node">Child Node to add to the Children collection.</param>
        /// <param name="unique">Only add if the item does not exist in the list already.</param>
        public void AddChild(Node<T> node, bool unique)
        {
            if (unique && ItemExists(node.Item, false, false))
            {
                return;
            }

            node.Parent = this;
            Children.Add(node);
        }

        /// <summary>
        /// Add multiple items to the Children collection.
        /// </summary>
        /// <param name="items">Items to add.</param>
        /// <param name="unique">Only add items if they do not exist in the list already.</param>
        public void AddChildren(IEnumerable<T> items, bool unique)
        {
            foreach (T item in items)
            {
                AddChild(item, unique);
            }
        }

        /// <summary>
        /// Add multiple nodes to the Children collection.
        /// </summary>
        /// <param name="nodes">Nodes to add.</param>
        /// <param name="unique">Only add items if they do not exist in the list already.</param>
        public void AddChildren(IEnumerable<Node<T>> nodes, bool unique)
        {
            foreach (Node<T> node in nodes)
            {
                AddChild(node, unique);
            }
        }

        /// <summary>
        /// Gets a new copy of this node.
        /// </summary>
        /// <param name="parent">The new parent to assign to the copy.</param>
        /// <param name="withChildren">Whether the node should come with copies of its children.</param>
        /// <returns>A copy of this node.</returns>
        public Node<T> GetCopy(Node<T> parent, bool withChildren)
        {
            Node<T> copy = new Node<T>(Item, parent);

            if (withChildren)
            {
                foreach (Node<T> child in Children)
                {
                    copy.AddChild(child.GetCopy(copy, withChildren), false);
                }
            }

            return copy;
        }

        /// <summary>
        /// Get the number of children.
        /// </summary>
        /// <param name="recursive">Include all ancestors.</param>
        /// <param name="unique">Only get the number of unique Items.</param>
        /// <param name="fromRoot">Get the value from the Root Node.</param>
        /// <returns>Number of children.</returns>
        public int GetCount(bool recursive, bool unique, bool fromRoot)
        {
            if (fromRoot)
            {
                return Root.GetCount(recursive, unique, false);
            }

            if (unique)
            {
                return GetItems(recursive, unique, false).Count;
            }

            int i = Children.Count;

            if (recursive)
            {
                foreach (Node<T> child in Children)
                {
                    i += child.GetCount(recursive, unique, false);
                }
            }

            return i;
        }

        /// <summary>
        /// Gets an existing instance of an Item if it exists.
        /// </summary>
        /// <param name="item">Item to search for.</param>
        /// <param name="recursive">Search all ancestors.</param>
        /// <param name="fromRoot">Start search from Root Node.</param>
        /// <returns>The existing instance of the Item.</returns>
        public T GetExistingItem(T item, bool recursive, bool fromRoot)
        {
            return GetExistingNode(item, recursive, fromRoot)?.Item;
        }

        /// <summary>
        /// Gets an existing Node that contains the same Item.
        /// </summary>
        /// <param name="node">Node with item to search for.</param>
        /// <param name="recursive">Include ancestors in search.</param>
        /// <param name="fromRoot">Start search from the Root Node.</param>
        /// <returns>A Node with an Item of equal value, null if not found.</returns>
        public Node<T> GetExistingNode(Node<T> node, bool recursive, bool fromRoot)
        {
            return GetExistingNode(node.Item, recursive, fromRoot);
        }

        /// <summary>
        /// Gets an existing Node that contains the same Item.
        /// </summary>
        /// <param name="item">Item within Node to search for.</param>
        /// <param name="recursive">Include ancestors in search.</param>
        /// <param name="fromRoot">Start search from the Root Node.</param>
        /// <returns>A Node with an Item of equal value, null if not found.</returns>
        public Node<T> GetExistingNode(T item, bool recursive, bool fromRoot)
        {
            if (fromRoot)
            {
                return Root.GetExistingNode(item, recursive, false);
            }

            foreach (Node<T> child in Children)
            {
                if (child.Item.Equals(item))
                {
                    return child;
                }
            }

            if (recursive)
            {
                foreach (Node<T> child in Children)
                {
                    Node<T> existingNode = child.GetExistingNode(item, recursive, false);

                    if (existingNode != null)
                    {
                        return existingNode;
                    }
                }
            }

            return null;
        }

        /// <summary>
        /// Get a list of the Items in this Node.
        /// </summary>
        /// <param name="recursive">Get all ancestor items.</param>
        /// <param name="unique">Only get one instance of each item.</param>
        /// <param name="fromRoot">Get items from the Root of this Node's tree.</param>
        /// <returns>A list of Items in the Node.</returns>
        public List<T> GetItems(bool recursive, bool unique, bool fromRoot)
        {
            if (fromRoot)
            {
                return Root.GetItems(recursive, unique, false);
            }

            List<T> items = new List<T>();

            foreach (Node<T> child in Children)
            {
                if (unique)
                {
                    if (!items.Contains(child.Item))
                    {
                        items.Add(child.Item);
                    }

                    if (recursive)
                    {
                        foreach (T item in child.GetItems(recursive, unique, false))
                        {
                            if (!items.Contains(item))
                            {
                                items.Add(item);
                            }
                        }
                    }
                }
                else
                {
                    items.Add(child.Item);

                    if (recursive)
                    {
                        items.AddRange(child.GetItems(recursive, unique, false));
                    }
                }
            }

            return items;
        }

        /// <summary>
        /// Checks if an Item already exists within the tree.
        /// </summary>
        /// <param name="item">Item to search for.</param>
        /// <param name="recursive">Search all ancestors.</param>
        /// <param name="fromRoot">Start search the Root Node.</param>
        /// <returns>True if the item exists.</returns>
        public bool ItemExists(T item, bool recursive, bool fromRoot)
        {
            if (fromRoot)
            {
                return Root.ItemExists(item, recursive, false);
            }

            if (Children.Any(child => child.Item.Equals(item)))
            {
                return true;
            }

            if (recursive)
            {
                return Children.Any(child => child.ItemExists(item, recursive, false));
            }

            return false;
        }
    }
}
