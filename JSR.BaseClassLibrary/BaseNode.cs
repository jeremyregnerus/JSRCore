// <copyright file="BaseNode.cs" company="Jeremy Regnerus">
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
    public class BaseNode<T> : BaseClass, INode<T> where T : BaseClass
    {
        private T item;
        private INode<T> parent;

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseNode{T}"/> class.
        /// </summary>
        /// <param name="item">Item contained within this node.</param>
        public BaseNode(T item)
        {
            Item = item;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseNode{T}"/> class.
        /// </summary>
        /// <param name="item">Item contained within this node.</param>
        /// <param name="parent">Parent Node of this Node.</param>
        public BaseNode(T item, INode<T> parent) : this(item)
        {
            Parent = parent;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseNode{T}"/> class.
        /// </summary>
        /// <param name="item">Item contained within this node.</param>
        /// <param name="children">Children Nodes of this Node.</param>
        public BaseNode(T item, IEnumerable<INode<T>> children) : this(item)
        {
            Children = new BaseCollection<INode<T>>(children);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseNode{T}"/> class.
        /// </summary>
        /// <param name="item">Item contained within this node.</param>
        /// <param name="parent">Parent Node of this Node.</param>
        /// <param name="children">Children Nodes of this Node.</param>
        public BaseNode(T item, INode<T> parent, IEnumerable<INode<T>> children) : this(item, parent)
        {
            Children = new BaseCollection<INode<T>>(children);
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
        public virtual BaseCollection<INode<T>> Children { get; } = new BaseCollection<INode<T>>();

        /// <summary>
        /// Gets or sets the Parent to this Node.
        /// </summary>
        public virtual INode<T> Parent { get => parent; set => SetValue(value, ref parent); }

        /// <summary>
        /// Gets a value indicating whether this Node has Child components.
        /// </summary>
        public bool HasChildren { get => Children.Count > 0; }

        /// <summary>
        /// Gets the Root node for the tree that this Node is contained within.
        /// </summary>
        public virtual INode<T> Root { get => Parent is null ? this : Parent.Root; }

        /// <summary>
        /// Adds a new child Node with an Item to the Children collection.
        /// </summary>
        /// <param name="item">The Item object within the new Child Node.</param>
        /// <param name="unique">Only add items if they do not exist in the list already.</param>
        public virtual void AddChild(T item, bool unique)
        {
            AddChild(new BaseNode<T>(item), unique);
        }

        /// <summary>
        /// Add a Child Node to the Children collection.
        /// </summary>
        /// <param name="node">Child Node to add to the Children collection.</param>
        /// <param name="unique">Only add if the item does not exist in the list already.</param>
        public virtual void AddChild(INode<T> node, bool unique)
        {
            if (node.Parent == this)
            {
                return;
            }

            if (ItemExists(node.Item, false))
            {
                return;
            }

            if (node.Parent == null)
            {
                node.Parent = this;
                Children.Add(node);
            }
        }

        /// <summary>
        /// Add multiple items to the Children collection.
        /// </summary>
        /// <param name="items">Items to add.</param>
        /// <param name="unique">Only add items if they do not exist in the list already.</param>
        public void AddRange(IEnumerable<T> items, bool unique)
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
        public virtual void AddRange(IEnumerable<INode<T>> nodes, bool unique)
        {
            foreach (INode<T> node in nodes)
            {
                AddChild(node, unique);
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
                    foreach (INode<T> child in Children)
                    {
                        i += child.GetCount(recursive, unique);
                    }
                }

                return i;
            }

            return GetItems(recursive, unique).Count;
        }

        /// <summary>
        /// Gets a new copy of this node.
        /// </summary>
        /// <param name="parent">The new parent to assign to the copy.</param>
        /// <param name="withChildren">Whether the node should come with copies of its children.</param>
        /// <returns>A copy of this node.</returns>
        public virtual INode<T> GetCopy(INode<T> parent, bool withChildren = true)
        {
            BaseNode<T> copy = new BaseNode<T>(Item, parent);

            if (withChildren)
            {
                foreach (INode<T> child in Children)
                {
                    copy.AddChild(child.GetCopy(copy, withChildren), false);
                }
            }

            return copy;
        }

        /// <summary>
        /// Get a list of the Items in this Node.
        /// </summary>
        /// <param name="recursive">Get all ancestor items.</param>
        /// <param name="unique">Only get one instance of each item.</param>
        /// <param name="fromRoot">Get items from the Root of this Node's tree.</param>
        /// <returns>A list of Items in the Node.</returns>
        public List<T> GetItems(bool recursive, bool unique, bool fromRoot = false)
        {
            if (fromRoot)
            {
                return Root.GetItems(recursive, unique);
            }

            List<T> items = new List<T>();

            foreach (INode<T> child in Children)
            {
                if (!unique)
                {
                    items.Add(child.Item);

                    if (recursive)
                    {
                        items.AddRange(child.GetItems(recursive, unique));
                    }

                    continue;
                }

                if (!items.Contains(child.Item))
                {
                    items.Add(child.Item);

                    if (recursive)
                    {
                        foreach (T item in child.GetItems(recursive, unique))
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

        /// <summary>
        /// Checks if the items already exists within the tree.
        /// </summary>
        /// <param name="item">Item to evaluate.</param>
        /// <param name="recursive">Check all ancestors.</param>
        /// <param name="fromRoot">Start from the Root Node.</param>
        /// <returns>True if the item exists.</returns>
        public bool ItemExists(T item, bool recursive, bool fromRoot = false)
        {
            if (fromRoot)
            {
                return Root.ItemExists(item, recursive);
            }

            if (Children.Any(child => child.Item.Equals(item)))
            {
                return true;
            }

            if (!recursive)
            {
                return false;
            }

            return Children.Any(child => child.ItemExists(item, recursive));
        }
    }
}
